using CsvHelper;
using LiveNet.Database.Context;
using LiveNet.Database.Mappers;
using LiveNet.Domain.Models;
using LiveNet.Infrastructure;
using LiveNet.Services.Dtos;
using LiveNet.Services.Expressions;
using LiveNet.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace LiveNet.Services.Services;

public class ContatoService(ApplicationDbContext context,
    IUsuarioAtualService usuarioAtualService) : IContatoService
{
    private readonly ApplicationDbContext _context = context;
    private readonly IUsuarioAtualService _usuarioAtualService = usuarioAtualService;

    public async Task<List<ContatoDto>> BuscarContatosAsync()
    {
        return await _context.Contatos
            .Select(ContatoExpressions.ToContatoDto)
            .ToListAsync();

    }

    public async Task<ImportacaoContatoDto> UploadListaAsync(IFormFile file)
    {

        var nome = Path.GetFileName(file.FileName);
        //Criação da da listagem 
        using var stream = file.OpenReadStream();
        using var reader = new StreamReader(stream);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        await SaveFileAsync(file);
        csv.Context.RegisterClassMap<ContatoMapper>();
        var contatos = csv.GetRecords<ContatoModel>().ToList();

        var emailsDuplicados = new HashSet<string>();
        var totalImportados = 0;

        foreach (var chunk in contatos.Chunk(40))
        {

            //Seleciona os Emails do chunk
            var emails = chunk
                .Where(e => !string.IsNullOrWhiteSpace(e.EmailEmpresa))
                .Select(c => c.EmailEmpresa!.Trim().ToLower())
                .Distinct()
                .ToList();

            //busca a lista de emails existentes na tabela de contatos
            var existentes = await context.Contatos
                .Where(c => emails.Contains(c.EmailEmpresa!))
                .Select(c => c.EmailEmpresa)
                .ToListAsync();

            //Converte para HashSet
            var existentesSet = existentes.ToHashSet();

            //Valida emails que não tem o mesmo hashSet
            var novos = chunk
                .Where(c =>
                    string.IsNullOrWhiteSpace(c.EmailEmpresa) ||
                    !existentesSet.Contains(c.EmailEmpresa.Trim().ToLower()))
                .ToList();

            var duplicados = emails
                .Where(e => existentesSet.Contains(e))
                .ToList();

            //Pensar na possiblidade de importar também Interesses e serviços (ficou muito complicado)

            foreach (var email in duplicados)
            {
                emailsDuplicados.Add(email);
            }

            foreach (var contato in novos)
            {
                contato.ModoInclusao = nome;
            }

            totalImportados += novos.Count;
            context.Contatos.AddRange(novos);
            await context.SaveChangesAsync();
        }

        return new ImportacaoContatoDto
        {
            TotalImportados = totalImportados,
            EmailsDuplicados = emailsDuplicados.ToList()
        };
    }

    public async Task<bool> CriarContatoManualAsync(ContatoModel contato)
    {
        var filtro = _context.Contatos.FirstOrDefaultAsync(predicate: f => f.EmailEmpresa == contato.EmailEmpresa || f.EmailPessoal == contato.EmailPessoal);
        if (filtro != null) return false;

        contato.ModoInclusao = "Manual";
        _context.Contatos.Add(contato);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> AtualizarContatoAsync(Guid id, ContatoModel contato)
    {
        var original = await _context.Contatos.FindAsync(id)
        ?? throw new KeyNotFoundException("Contato não encontrado");

        var usuarioId = _usuarioAtualService.UsuarioId
            ?? throw new UnauthorizedAccessException();

        EntityDiffValidate.ValidarDif(original, contato);

        original.UpdatedAt = DateTimeOffset.Now;
        original.UpdatedBy = usuarioId;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExcluirContatoAsync(int id)
    {
        var contatoExcluido = await _context.Contatos.FindAsync(id)
        ?? throw new KeyNotFoundException("Contato não encontrado");

        var usuarioId = _usuarioAtualService.UsuarioId
            ?? throw new UnauthorizedAccessException();

        contatoExcluido.IsDeleted = true;
        contatoExcluido.DeletedBy = usuarioId;
        contatoExcluido.DeletedAt = DateTimeOffset.Now;

        await _context.SaveChangesAsync();
        return true;
    }

    private static async Task SaveFileAsync(IFormFile file)
    {
        var pasta = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
        Directory.CreateDirectory(pasta);

        var fileName = $"/{file.FileName}";
        var filePath = Path.Combine(pasta, fileName);

        using var fileStream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(fileStream);
    }
}

using CsvHelper;
using LiveNet.Database.Context;
using LiveNet.Database.Mappers;
using LiveNet.Domain.Models;
using LiveNet.Infrastructure;
using LiveNet.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Data.Entity;
using System.Globalization;

namespace LiveNet.Services.Services;

public class ContatoService(ApplicationDbContext context,
    UsuarioAtualService usuarioAtualService) : IContatoService
{
    private readonly ApplicationDbContext _context = context;
    private readonly UsuarioAtualService _usuarioAtualService = usuarioAtualService;

    public async Task<List<ContatoModel>> BuscarContatosAsync()
    {
        var ret = await _context.Contato.ToListAsync();
        return ret;
    }

    public async Task<bool> UploadListaAsync(IFormFile file, string nome)
    {
        using var stream = file.OpenReadStream();
        using var reader = new StreamReader(stream);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        await SaveFileAsync(file);
        csv.Context.RegisterClassMap<ContatoMapper>();
        var contatos = csv.GetRecords<ContatoModel>().ToList();

        foreach (var contato in contatos)
        {
            var filtro = _context.Contato.FirstOrDefaultAsync(predicate: f => f.EmailEmpresa == contato.EmailEmpresa || f.EmailPessoal == contato.EmailPessoal);
            if (filtro == null)
            {
                contato.ModoInclusao = nome;
                await _context.AddAsync(contato);
            }
            else { /*criar uma lista com os contatos não adicionados e apresentar no front*/}
        }

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> CriarContatoManualAsync(ContatoModel contato)
    {
        var filtro = _context.Contato.FirstOrDefaultAsync(predicate: f => f.EmailEmpresa == contato.EmailEmpresa || f.EmailPessoal == contato.EmailPessoal);
        if (filtro == null)
        {
            _context.Contato.Add(contato);
            contato.ModoInclusao = "Manual";
            await _context.SaveChangesAsync();
            return true;
        }
        else return false;
    }

    public async Task<bool> AtualizarContatoAsync(Guid id, ContatoModel contato)
    {
        var original = await _context.Contato.FindAsync(id);
        if (original == null) return false;

        EntityDiffValidate.ValidarDif(original, contato);

        original.UpdatedAt = DateTimeOffset.Now;
        original.UpdatedBy = _usuarioAtualService.UsuarioId;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExcluirContatoAsync(int id)
    {
        var contatoExcluido = await _context.Contato.FindAsync(id);
        if (contatoExcluido != null)
        {
            contatoExcluido.IsDeleted = true;
            contatoExcluido.DeletedBy = _usuarioAtualService.UsuarioId;
            await _context.SaveChangesAsync();
            return true;
        }
        else
            return false;
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

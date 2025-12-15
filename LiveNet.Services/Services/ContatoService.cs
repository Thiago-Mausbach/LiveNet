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

public class ContatoService : IContatoService
{
    private readonly ApplicationDbContext _context;
    private readonly UsuarioAtualService _usuarioAtualService;

    public ContatoService(ApplicationDbContext context,
        UsuarioAtualService usuarioAtualService)
    {
        _context = context;
        _usuarioAtualService = usuarioAtualService;
    }

    public async Task<List<ContatoModel>> BuscarContatosAsync()
    {
        var teste = await _context.Contato.ToListAsync();
        return teste;
    }

    public async Task UploadListaAsync(IFormFile file, string nome)
    {
        using var stream = file.OpenReadStream();
        using var reader = new StreamReader(stream);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        await SaveFileAsync(file);
        csv.Context.RegisterClassMap<ContatoMapper>();
        var contatos = csv.GetRecords<ContatoModel>().ToList();

        foreach (var contato in contatos)
        {
            contato.ModoInclusao = nome;
            await _context.AddAsync(contato);
        }

        await _context.SaveChangesAsync();
    }

    public async Task CriarContatoManualAsync(ContatoModel contato)
    {
        _context.Contato.Add(contato);
        contato.ModoInclusao = "Manual";
        await _context.SaveChangesAsync();
    }

    public async Task<ContatoModel> AtualizarContatoAsync(int id, ContatoModel contato)
    {
        var original = await _context.Contato.FindAsync(id);
        if (original == null) return original;

        EntityDiffValidate.ValidarDif(original, contato);

        original.UpdatedAt = DateTimeOffset.Now;
        original.UpdatedBy = _usuarioAtualService.UsuarioId;
        await _context.SaveChangesAsync();
        return original;
    }

    public async Task<int> ExcluirContatoAsync(int id)
    {
        var contatoExcluido = await _context.Contato.FindAsync(id);
        if (contatoExcluido != null)
        {
            contatoExcluido.IsDeleted = true;
            contatoExcluido.DeletedBy = _usuarioAtualService.UsuarioId;
            await _context.SaveChangesAsync();
            return 1;
        }
        else
            return 0;
    }

    private async Task SaveFileAsync(IFormFile file)
    {
        var pasta = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
        Directory.CreateDirectory(pasta);

        var fileName = $"{Guid.NewGuid()}_{file.FileName}";
        var filePath = Path.Combine(pasta, fileName);

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }
    }
}

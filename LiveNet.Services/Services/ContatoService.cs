using CsvHelper;
using LiveNet.Database.Context;
using LiveNet.Database.Mappers;
using LiveNet.Domain.Models;
using LiveNet.Infrastructure;
using LiveNet.Services.Interfaces;
using System.Data.Entity;
using System.Globalization;

namespace LiveNet.Services.Services;

public class ContatoService : IContatoService
{
    private readonly ApplicationDbContext _context;
    private readonly UsuarioAtualService _usuarioAtualService;

    public async Task<List<ContatoModel>> BuscarContatosAsync()
    {
        var teste = await _context.Contato.ToListAsync();
        return teste;
    }
    public async Task CriarContatosListaAsync(Stream stream, string nome)
    {

        using var reader = new StreamReader(stream);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        csv.Context.RegisterClassMap<ContatoMapper>();
        var contatos = csv.GetRecords<ContatoModel>().ToList();

        foreach (var contato in contatos)
        {
            //TODO validação, regras de negócio, persistência
            contato.ModoInclusao = nome;
            await _context.AddAsync(contato);
        }
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
}

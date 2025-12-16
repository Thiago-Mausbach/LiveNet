using LiveNet.Database.Context;
using LiveNet.Domain.Models;
using LiveNet.Infrastructure;
using LiveNet.Services.Interfaces;
using System.Data.Entity;

namespace LiveNet.Services.Services;

internal class ServicoService(ApplicationDbContext context, UsuarioAtualService usuarioAtualService) : IServicoService
{
    private readonly ApplicationDbContext _context = context;
    private readonly UsuarioAtualService _usuarioAtualService = usuarioAtualService;


    public async Task<List<ServicoModel>> BuscarServicosAsync()
    {
        return await _context.Servico.ToListAsync();
    }

    public async Task CriarServicoAsync(ServicoModel servico)
    {
        _context.Servico.Add(servico);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> AtualizarServicoAsync(ServicoModel servico)
    {
        var original = await _context.Servico.FindAsync(servico.Id);
        if (original == null) return false;

        EntityDiffValidate.ValidarDif(original, servico);

        original.UpdatedAt = DateTimeOffset.Now;
        original.UpdatedBy = _usuarioAtualService.UsuarioId;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeletarServicoAsync(int id)
    {
        var servico = await _context.Servico.FirstOrDefaultAsync(x => x.Id == id);
        if (servico != null)
        {
            servico.IsDeleted = true;
            servico.DeletedAt = DateTime.Now;
            servico.DeletedBy = _usuarioAtualService.UsuarioId;
            await _context.SaveChangesAsync();
            return true;
        }
        else
        {
            return false;
        }
    }
}
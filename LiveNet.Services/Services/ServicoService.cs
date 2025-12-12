using LiveNet.Database.Context;
using LiveNet.Domain.Mapping;
using LiveNet.Domain.Models;
using LiveNet.Domain.ViewModels;
using LiveNet.Infrastructure;
using LiveNet.Services.Interfaces;
using System.Data.Entity;

namespace LiveNet.Services.Services;

internal class ServicoService : IServicoService
{
    private readonly ApplicationDbContext _context;
    private readonly UsuarioAtualService _usuarioAtualService;


    public async Task<List<ServicoViewModel>> BuscarServicosAsync()
    {
        return await _context.Servico.Select(s => s.ToServicoDto()).ToListAsync();
    }

    public async Task CriarServicoAsync(ServicoModel servico)
    {
        _context.Servico.Add(servico);
        await _context.SaveChangesAsync();
    }

    public async Task<ServicoModel> AtualizarServicoAsync(ServicoModel servico)
    {
        var original = await _context.Servico.FindAsync(servico.Id);
        if (original == null) return original;

        EntityDiffValidate.ValidarDif(original, servico);

        original.UpdatedAt = DateTimeOffset.Now;
        original.UpdatedBy = _usuarioAtualService.UsuarioId;
        await _context.SaveChangesAsync();
        return original;
    }

    public async Task<int> DeletarServicoAsync(int id)
    {
        var servico = await _context.Servico.FirstOrDefaultAsync(x => x.Id == id);
        if (servico != null)
        {
            servico.IsDeleted = true;
            servico.DeletedAt = DateTime.Now;
            servico.DeletedBy = _usuarioAtualService.UsuarioId;
            await _context.SaveChangesAsync();
            return 1;
        }
        else
        {
            return 0;
        }
    }
}
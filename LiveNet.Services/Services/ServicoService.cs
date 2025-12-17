using LiveNet.Database.Context;
using LiveNet.Domain.Models;
using LiveNet.Infrastructure;
using LiveNet.Services.Dtos;
using LiveNet.Services.Interfaces;
using System.Data.Entity;

namespace LiveNet.Services.Services;

internal class ServicoService(ApplicationDbContext context, UsuarioAtualService usuarioAtualService) : IServicoService
{
    private readonly ApplicationDbContext _context = context;
    private readonly UsuarioAtualService _usuarioAtualService = usuarioAtualService;


    public async Task<List<ServicoDto>> BuscarServicosAsync()
    {
        return await _context.Servicos
            .Select(s => new ServicoDto
            {
                Id = s.Id,
                Servico = s.Servico
            }).ToListAsync();

    }

    public async Task CriarServicoAsync(ServicoModel servico)
    {
        _context.Servicos.Add(servico);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> AtualizarServicoAsync(ServicoModel servico, Guid id)
    {
        var original = await _context.Servicos.FindAsync(servico.Id);
        if (original == null) return false;

        EntityDiffValidate.ValidarDif(original, servico);

        original.UpdatedAt = DateTimeOffset.Now;
        original.UpdatedBy = _usuarioAtualService.UsuarioId;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeletarServicoAsync(Guid id)
    {
        var servico = await _context.Servicos.FindAsync(id);
        if (servico == null) return false;

        servico.IsDeleted = true;
        servico.DeletedAt = DateTime.Now;
        servico.DeletedBy = _usuarioAtualService.UsuarioId;
        await _context.SaveChangesAsync();
        return true;
    }
}
using LiveNet.Database.Context;
using LiveNet.Domain.Models;
using LiveNet.Infrastructure;
using LiveNet.Services.Dtos;
using LiveNet.Services.Interfaces;
using System.Data.Entity;

namespace LiveNet.Services.Services;

public class EmpresaService(ApplicationDbContext context, UsuarioAtualService usuarioAtualService) : IEmpresaService
{
    private readonly ApplicationDbContext _context = context;
    private readonly UsuarioAtualService _usuarioAtualService = usuarioAtualService;
    public async Task<List<EmpresaDto>> ListarEmpresasAsync()
    {
        return await _context.Empresas
            .AsNoTracking().Select(e => new EmpresaDto
            {
                Id = e.Id,
                Cnpj = e.Cnpj,
                RazaoSocial = e.RazaoSocial,
            }).ToListAsync();
    }

    public async Task<bool> CriarEmpresaAsync(EmpresaModel empresa)
    {
        _context.Empresas.Add(empresa);
        var ret = await _context.SaveChangesAsync();
        if (ret > 0)
            return true;
        else
            return false;
    }

    public async Task<bool> AtualizarEmpresaAsync(Guid id, EmpresaModel empresa)
    {
        var original = await _context.Empresas.FindAsync(id);
        if (original == null) return false;

        EntityDiffValidate.ValidarDif(original, empresa);

        original.UpdatedAt = DateTimeOffset.Now;
        original.UpdatedBy = _usuarioAtualService.UsuarioId;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RemoverEmpresaAsync(Guid id)
    {
        var empresa = await _context.Empresas.FindAsync(id);
        if (empresa == null)
            return false;
        else
        {
            empresa.DeletedAt = DateTimeOffset.Now;
            empresa.DeletedBy = _usuarioAtualService.UsuarioId;
            empresa.IsDeleted = true;
            return true;
        }
    }
}
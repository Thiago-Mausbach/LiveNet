using LiveNet.Database.Context;
using LiveNet.Domain.Models;
using LiveNet.Infrastructure;
using LiveNet.Services.Dtos;
using LiveNet.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LiveNet.Services.Services;

public class EmpresaService(ApplicationDbContext context, IUsuarioAtualService usuarioAtualService) : IEmpresaService
{
    private readonly ApplicationDbContext _context = context;
    private readonly IUsuarioAtualService _usuarioAtualService = usuarioAtualService;
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
        var original = await _context.Empresas.FindAsync(id)
            ?? throw new KeyNotFoundException("Empresa não encontrada");

        var usuarioId = _usuarioAtualService.UsuarioId
            ?? throw new UnauthorizedAccessException();

        EntityDiffValidate.ValidarDif(original, empresa);

        original.UpdatedAt = DateTimeOffset.Now;
        original.UpdatedBy = usuarioId;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RemoverEmpresaAsync(Guid id)
    {
        var empresa = await _context.Empresas.FindAsync(id)
        ?? throw new KeyNotFoundException("Empresa não encontrada");

        var usuarioId = _usuarioAtualService.UsuarioId
            ?? throw new UnauthorizedAccessException();

        empresa.DeletedAt = DateTimeOffset.Now;
        empresa.IsDeleted = true;
        empresa.DeletedBy = usuarioId;

        await _context.SaveChangesAsync();
        return true;
    }
}
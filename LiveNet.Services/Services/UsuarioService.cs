using LiveNet.Database.Context;
using LiveNet.Domain.Models;
using LiveNet.Infrastructure;
using LiveNet.Services.Interfaces;
using System.Data.Entity;

namespace LiveNet.Services.Services;

public class UsuarioService(ApplicationDbContext context, UsuarioAtualService usuarioAtualService) : IUsuarioService
{

    private readonly ApplicationDbContext _context = context;
    private readonly UsuarioAtualService _usuarioAtualService = usuarioAtualService;

    public async Task<List<UsuarioModel>> BuscarUsuariosAsync()
    {
        return await _context.Usuarios.ToListAsync();
    }

    public async Task CriarUsuarioAsync(UsuarioModel usuario)
    {
        _context.Add(usuario);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> EditarUsuarioAsync(UsuarioModel usuario)
    {
        var original = await _context.Usuarios.FindAsync(usuario.Id);
        if (original == null) return false;

        EntityDiffValidate.ValidarDif(original, usuario);

        original.UpdatedAt = DateTimeOffset.Now;
        original.UpdatedBy = _usuarioAtualService.UsuarioId;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeletarUsuariosAsync(Guid id)
    {
        var servico = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
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
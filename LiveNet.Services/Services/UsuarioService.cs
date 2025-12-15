using LiveNet.Database.Context;
using LiveNet.Domain.Models;
using LiveNet.Infrastructure;
using LiveNet.Services.Interfaces;
using System.Data.Entity;

namespace LiveNet.Services.Services;

public class UsuarioService : IUsuarioService
{

    private readonly ApplicationDbContext _context;

    public async Task<List<UsuarioModel>> BuscarUsuariosAsync()
    {
        return await _context.Usuario.ToListAsync();
    }

    public async Task CriarUsuarioAsync(UsuarioModel usuario)
    {
        _context.Add(usuario);
        await _context.SaveChangesAsync();
    }

    public async Task<UsuarioModel> EditarUsuarioAsync(UsuarioModel usuario)
    {
        var original = await _context.Usuario.FindAsync(usuario.Id);
        if (original == null) return original;

        EntityDiffValidate.ValidarDif(original, usuario);

        original.UpdatedAt = DateTimeOffset.Now;
        await _context.SaveChangesAsync();
        return usuario;
    }

    public async Task<int> DeletarUsuariosAsync(Guid id)
    {
        var servico = await _context.Usuario.FirstOrDefaultAsync(x => x.Id == id);
        if (servico != null)
        {
            servico.IsDeleted = true;
            servico.DeletedAt = DateTime.Now;
            return 1;
            //TODO ver como capturar o id do usuário alterando
        }
        else
        {
            return 0;
        }
    }
}
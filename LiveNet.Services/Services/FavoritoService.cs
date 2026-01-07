using LiveNet.Database.Context;
using LiveNet.Domain.Models;
using LiveNet.Services.Dtos;
using LiveNet.Services.Expressions;
using LiveNet.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LiveNet.Services.Services;

public class FavoritoService(ApplicationDbContext context, IUsuarioAtualService usuarioAtualService) : IFavoritoService
{
    private readonly ApplicationDbContext _context = context;
    private readonly IUsuarioAtualService _usuarioAtualService = usuarioAtualService;

    public async Task<IEnumerable<ContatoDto>> ListarFavoritosAsync()
    {

        var favoritos = await _context.Favoritos
            .Where(x => x.UsuarioId == _usuarioAtualService.UsuarioId   )
            .Select(x => x.Contato).ToListAsync();

            return favoritos
            .Select(ContatoExpressions.ToContatoDto).ToList();
    }

    public async Task<bool> ToggleAsync(Guid contatoId, Guid usuarioId)
    {

        if (await _context.Usuarios.FindAsync(usuarioId) == null)
            throw new UnauthorizedAccessException();

        var favorito = await _context.Favoritos
            .FirstOrDefaultAsync(f =>
                f.UsuarioId == usuarioId &&
                f.ContatoId == contatoId);

        if (favorito != null)
        {
            _context.Favoritos.Remove(favorito);
            await _context.SaveChangesAsync();
            return false;
        }

        _context.Favoritos.Add(new FavoritosModel
        {
            UsuarioId = usuarioId,
            ContatoId = contatoId
        });

        await _context.SaveChangesAsync();
        return true;
    }
}

using LiveNet.Database.Context;
using LiveNet.Domain.Models;
using LiveNet.Services.Dtos;
using LiveNet.Services.Expressions;
using LiveNet.Services.Interfaces;
using System.Data.Entity;

namespace LiveNet.Services.Services;

public class FavoritoService(ApplicationDbContext context, UsuarioAtualService usuarioAtualService) : IFavoritoService
{
    private readonly ApplicationDbContext _context = context;
    private readonly UsuarioAtualService _usuarioAtualService = usuarioAtualService;

    public async Task<IEnumerable<ContatoDto>> ListarFavoritosAsync()
    {
        return await _context.Favoritos
            .Where(x => x.UsuarioId == _usuarioAtualService.UsuarioId)
            .Select(x => x.Contato)
            .Select(ContatoExpressions.ToContatoDto).ToListAsync();
    }

    public async Task<bool> ToggleAsync(Guid contatoId)
    {
        var usuarioId = _usuarioAtualService.UsuarioId
            ?? throw new UnauthorizedAccessException();

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
            Id = Guid.NewGuid(),
            UsuarioId = usuarioId,
            ContatoId = contatoId
        });

        await _context.SaveChangesAsync();
        return true;
    }
}

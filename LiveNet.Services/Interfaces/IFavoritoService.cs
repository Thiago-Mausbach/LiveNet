using LiveNet.Domain.Models;

namespace LiveNet.Services.Interfaces;

public interface IFavoritoService
{
    Task<IEnumerable<ContatoModel>> ListarFavoritosAsync();
    Task<bool> ToggleAsync(Guid contatoId);
}

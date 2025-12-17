using LiveNet.Services.Dtos;

namespace LiveNet.Services.Interfaces;

public interface IFavoritoService
{
    Task<IEnumerable<ContatoDto>> ListarFavoritosAsync();
    Task<bool> ToggleAsync(Guid contatoId);
}

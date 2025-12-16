using LiveNet.Domain.Models;

namespace LiveNet.Services.Interfaces;

public interface IUsuarioService
{
    Task<List<UsuarioModel>> BuscarUsuariosAsync();
    Task CriarUsuarioAsync(UsuarioModel usuario);
    Task<bool> EditarUsuarioAsync(UsuarioModel usuario);
    Task<bool> DeletarUsuariosAsync(Guid id);
}

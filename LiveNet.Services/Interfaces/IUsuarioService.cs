using LiveNet.Domain.Models;

namespace LiveNet.Services.Interfaces;

public interface IUsuarioService
{
    Task<List<UsuarioModel>> BuscarUsuariosAsync();
    Task CriarUsuarioAsync(UsuarioModel usuario);
    Task<UsuarioModel> EditarUsuarioAsync(UsuarioModel usuario);
    Task<int> DeletarUsuariosAsync(Guid id);
}

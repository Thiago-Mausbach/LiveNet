using LiveNet.Domain.Models;
using LiveNet.Domain.ViewModels;

namespace LiveNet.Services.Interfaces;

public interface IUsuarioService
{
    Task<List<UsuarioViewModel>> BuscarUsuariosAsync();
    Task CriarUsuarioAsync(UsuarioModel usuario);
    Task<UsuarioModel> EditarUsuarioAsync(UsuarioModel usuario);
    Task<int> DeletarUsuariosAsync(Guid id);
}

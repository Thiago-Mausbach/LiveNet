using LiveNet.Domain.Models;
using LiveNet.Services.Dtos;

namespace LiveNet.Services.Interfaces;

public interface IUsuarioService
{
    Task<List<UsuarioDto>> BuscarUsuariosAsync();
    Task CriarUsuarioAsync(UsuarioModel usuario);
    Task<bool> EditarUsuarioAsync(UsuarioModel usuario, Guid id);
    Task<bool> DeletarUsuariosAsync(Guid id);
}

using LiveNet.Services.Dtos;

namespace LiveNet.Services.Interfaces;

public interface IUsuarioService
{
    Task<List<UsuarioDto>> BuscarUsuariosAsync();
    Task<UsuarioResponseDto> CriarUsuarioAsync( UsuarioDto usuario );
    Task<bool> EditarUsuarioAsync( UsuarioDto usuario, Guid id );
    Task<bool> DeletarUsuariosAsync( Guid id );
}

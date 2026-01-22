using LiveNet.Services.Dtos;

namespace LiveNet.Services.Interfaces;

public interface IEmpresaService
{
    Task<List<EmpresaDto>> ListarEmpresasAsync();
    Task<bool> CriarEmpresaAsync( EmpresaDto empresa );
    Task<bool> AtualizarEmpresaAsync( Guid id, EmpresaDto empresa );
    Task<bool> RemoverEmpresaAsync( Guid id );
}

using LiveNet.Domain.Models;
using LiveNet.Services.Dtos;

namespace LiveNet.Services.Interfaces;

public interface IEmpresaService
{
    Task<List<EmpresaDto>> ListarEmpresasAsync();
    Task<bool> CriarEmpresaAsync(EmpresaModel empresa);
    Task<bool> AtualizarEmpresaAsync(Guid id, EmpresaModel empresa);
    Task<bool> RemoverEmpresaAsync(Guid id);
}

using LiveNet.Domain.Models;

namespace LiveNet.Services.Interfaces;

public interface IEmpresaService
{
    Task<List<EmpresaModel>> ListarEmpresasAsync();
    Task<bool> CriarEmpresaAsync(EmpresaModel empresa);
    Task<bool> AtualizarEmpresaAsync(Guid id, EmpresaModel empresa);
    Task<bool> RemoverEmpresaAsync(Guid id);
}

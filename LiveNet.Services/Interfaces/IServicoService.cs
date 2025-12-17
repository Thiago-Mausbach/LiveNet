using LiveNet.Domain.Models;
using LiveNet.Services.Dtos;

namespace LiveNet.Services.Interfaces;

public interface IServicoService
{
    Task<List<ServicoDto>> BuscarServicosAsync();
    Task CriarServicoAsync(ServicoModel servico);
    Task<bool> AtualizarServicoAsync(ServicoModel servico, Guid id);
    Task<bool> DeletarServicoAsync(Guid id);
}

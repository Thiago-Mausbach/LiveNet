using LiveNet.Domain.Models;

namespace LiveNet.Services.Interfaces;

public interface IServicoService
{
    Task<List<ServicoModel>> BuscarServicosAsync();
    Task CriarServicoAsync(ServicoModel servico);
    Task<bool> AtualizarServicoAsync(ServicoModel servico);
    Task<bool> DeletarServicoAsync(Guid id);
}

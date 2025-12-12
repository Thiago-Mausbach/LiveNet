using LiveNet.Domain.Models;

namespace LiveNet.Services.Interfaces;

public interface IServicoService
{
    Task<List<ServicoModel>> BuscarServicosAsync();
    Task CriarServicoAsync(ServicoModel servico);
    Task<ServicoModel> AtualizarServicoAsync(ServicoModel servico);
    Task<int> DeletarServicoAsync(int id);
}

using LiveNet.Domain.Models;
using LiveNet.Domain.ViewModels;

namespace LiveNet.Services.Interfaces;

public interface IServicoService
{
    Task<List<ServicoViewModel>> BuscarServicosAsync();
    Task CriarServicoAsync(ServicoModel servico);
    Task<ServicoModel> AtualizarServicoAsync(ServicoModel servico);
    Task<int> DeletarServicoAsync(int id);
}

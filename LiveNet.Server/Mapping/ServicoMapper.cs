using LiveNet.Api.ViewModels;
using LiveNet.Domain.Models;

namespace LiveNet.Api.Mapping;

public class ServicoMapper
{
    public static ServicoModel ToServicoModel(ServicoViewModel viewModel)
    {
        return new ServicoModel
        {
            Id = viewModel.Id,
            Servico = viewModel.Servico,
        };
    }
}

using LiveNet.Api.ViewModels;
using LiveNet.Domain.Models;

namespace LiveNet.Api.Mapping;

public class EmpresaMapper
{
    public static EmpresaModel ToEmpresaModel(EmpresaViewModel viewModel)
    {
        return new EmpresaModel
        {
            Id = viewModel.Id,
            Cnpj = viewModel.Cnpj,
            RazaoSocial = viewModel.RazaoSocial
        };
    }
}
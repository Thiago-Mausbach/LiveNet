using LiveNet.Api.ViewModels;
using LiveNet.Domain.Models;

namespace LiveNet.Api.Mapping;

public class UsuarioMapper
{
    public static UsuarioModel ToUsuarioModel(UsuarioViewModel viewModel)
    {
        return new UsuarioModel
        {
            Nome = viewModel.Nome,
            Email = viewModel.Email,
            Senha = viewModel.Senha,
            Id = viewModel.Id
        };
    }
}

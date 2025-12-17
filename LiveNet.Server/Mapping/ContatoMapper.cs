using LiveNet.Api.ViewModels;
using LiveNet.Domain.Models;

namespace LiveNet.Api.Mapping;

public static class ContatoMapper
{
    public static ContatoModel ToContatoModel(
        ContatoViewModel viewModel,
        Guid empresaId
    )
    {
        return new ContatoModel
        {
            Id = viewModel.Id,
            Nome = viewModel.Nome,
            Cpf = viewModel.Cpf,
            EmailEmpresa = viewModel.EmailEmpresa,
            EmailPessoal = viewModel.EmailPessoal,
            Telefone = viewModel.Telefone,
            Cargo = viewModel.Cargo,
            Cliente = viewModel.Cliente,
            ModoInclusao = viewModel.ModoInclusao,
            CnpjEmpresa = viewModel.CnpjEmpresa,

            Interesses = viewModel.Interesses?
                .Select(i => new ContatoInteresseModel
                {
                    InteresseId = i.Id
                })
                .ToList(),

            Servicos = viewModel.Servicos?
                .Select(s => new ContatoServicoModel
                {
                    ServicoId = s.Id
                })
                .ToList()
        };
    }
}
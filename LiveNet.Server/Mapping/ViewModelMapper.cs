using LiveNet.Api.ViewModels;
using LiveNet.Domain.Models;

namespace LiveNet.Api.Mapping;

public static class ViewModelMapper
{
    public static ContatoViewModel ToContatoDto(this ContatoModel value)
    {
        var result = new ContatoViewModel
        {
            Nome = value.Nome,
            EmailPessoal = value.EmailPessoal,
            Empresa = value.Empresa.RazaoSocial,
            EmailEmpresa = value.EmailEmpresa,
            Cargo = value.Cargo,
            Telefone = value.Telefone,
            Interesses = value.Interesses,
            ModoInclusao = value.ModoInclusao,
            Servicos = value.Servicos.Select(s => new ServicoViewModel
            {
                Id = s.Servico.Id,
                Servico = s.Servico.Servico
            }).ToList()
        };
        return result;
    }

    public static ServicoViewModel ToServicoDto(this ServicoModel value)
    {
        var result = new ServicoViewModel
        {
            Id = value.Id,
            Servico = value.Servico
        };
        return result;
    }

    public static UsuarioViewModel ToUsuarioDto(this UsuarioModel value)
    {
        var result = new UsuarioViewModel
        {
            Id = value.Id,
            Nome = value.Nome,
            Email = value.Email,
            Senha = value.Senha
        };
        return result;
    }

    public static EmpresaViewModel ToEmpresaDto(this EmpresaModel value)
    {
        var result = new EmpresaViewModel
        {
            Id = value.Id,
            RazaoSocial = value.RazaoSocial,
            Cnpj = value.Cnpj,
        };
        return result;
    }
}
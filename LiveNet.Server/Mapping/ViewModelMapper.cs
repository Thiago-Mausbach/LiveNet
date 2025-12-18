using LiveNet.Api.ViewModels;
using LiveNet.Domain.Models;
using LiveNet.Services.Dtos;

namespace LiveNet.Api.Mapping;

public static class ViewModelMapper
{
    public static ContatoViewModel ToContatoDto(this ContatoModel value)
    {
        var result = new ContatoViewModel
        {
            Nome = value.Nome,
            EmailPessoal = value.EmailPessoal,
            CnpjEmpresa = value.CnpjEmpresa,
            Empresa = value.Empresa.RazaoSocial,
            EmailEmpresa = value.EmailEmpresa,
            Cargo = value.Cargo,
            Telefone = value.Telefone,
            Interesses = value.Interesses.Select(i => new InteresseViewModel
            {
                Id = i.Id,
                Interesse = i.Interesse.Interesse
            }).ToList(),
            ModoInclusao = value.ModoInclusao,
            Servicos = value.Servicos.Select(s => new ServicoViewModel
            {
                Id = s.Servico.Id,
                Servico = s.Servico.Servico
            }).ToList()
        };
        return result;
    }

    public static ContatoViewModel ToContatoVm(this ContatoDto value)
    {
        var result = new ContatoViewModel
        {
            Nome = value.Nome,
            EmailPessoal = value.EmailPessoal,
            CnpjEmpresa = value.CnpjEmpresa,
            Empresa = value.Empresa,
            EmailEmpresa = value.EmailEmpresa,
            Cargo = value.Cargo,
            Telefone = value.Telefone,
            Interesses = value.Interesses.Select(i => new InteresseViewModel
            {
                Id = i.Id,
                Interesse = i.Interesse
            }).ToList(),
            ModoInclusao = value.ModoInclusao,
            Servicos = value.Servicos.Select(s => new ServicoViewModel
            {
                Id = s.Id,
                Servico = s.Servico
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

    public static InteresseViewModel ToInteresseDto(this InteresseModel value)
    {
        var result = new InteresseViewModel
        {
            Interesse = value.Interesse,
            Id = value.Id
        };
        return result;
    }
}
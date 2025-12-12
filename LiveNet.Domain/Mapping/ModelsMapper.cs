using LiveNet.Domain.Models;
using LiveNet.Domain.ViewModels;

namespace LiveNet.Domain.Mapping;

public static class ModelsMapper
{
    public static ContatoViewModel ToContatoDto(this ContatoModel value)
    {
        var result = new ContatoViewModel
        {
            Nome = value.Nome,
            EmailPessoal = value.EmailPessoal,
            Empresa = value.Empresa,
            EmailEmpresa = value.EmailEmpresa,
            Cargo = value.Cargo,
            Telefone = value.Telefone,
            Interesses = value.Interesses,
            ModoInclusao = value.ModoInclusao,
            Servicos = value.Servicos.Select(s => new ServicoViewModel
            {
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
        };
        return result;
    }
}
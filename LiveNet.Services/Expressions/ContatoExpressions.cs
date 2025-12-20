using LiveNet.Domain.Models;
using LiveNet.Services.Dtos;
using System.Linq.Expressions;

namespace LiveNet.Services.Expressions;

public static class ContatoExpressions
{
    public static ContatoDto ToContatoDto(ContatoModel c)
    {

        return new ContatoDto
        {
            Id = c.Id,
            Nome = c.Nome,
            Cpf = c.Cpf,
            EmailEmpresa = c.EmailEmpresa,
            EmailPessoal = c.EmailPessoal,
            Telefone = c.Telefone,

            Empresa = c.Empresa?.RazaoSocial,

            CnpjEmpresa = c.CnpjEmpresa,
            Cargo = c.Cargo,
            Cliente = c.Cliente,
            ModoInclusao = c.ModoInclusao,

            Interesses = c.Interesses?
                                .Select(ci => new InteresseDto
                                {
                                    Id = ci.Interesse.Id,
                                    Interesse = ci.Interesse.Interesse
                                })
                                .ToList(),

            Servicos = c.Servicos?
                                .Select(cs => new ServicoDto
                                {
                                    Id = cs.Servico.Id,
                                    Servico = cs.Servico.Servico
                                })
                                .ToList()
        };
    }
}

using CsvHelper.Configuration;
using LiveNet.Domain.Models;

namespace LiveNet.Database.Mappers;

public sealed class ContatoMapper : ClassMap<ContatoModel>
{
    public ContatoMapper()
    {
        Map(m => m.Nome).Name("Nome");
        Map(m => m.Cpf).Name("Cpf");
        Map(m => m.EmailEmpresa).Name("EmailEmpresa");
        Map(m => m.EmailPessoal).Name("EmailPessoal");
        Map(m => m.Telefone).Name("Telefone");
        Map(m => m.Empresa).Name("Empresa");
        Map(m => m.CnpjEmpresa).Name("Cnpj");
        Map(m => m.Cargo).Name("Cargo");
    }
}
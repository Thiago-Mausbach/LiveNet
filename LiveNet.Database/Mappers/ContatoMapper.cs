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
        Map(m => m.Interesses)
            .Name("Interesses")
            .Convert(args =>
                args.Row.GetField("Interesses")
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .ToList());
        Map(m => m.Servicos)
    .Name("Servicos")
    .Convert(args =>
    {
        var csvValue = args.Row.GetField("Servicos"); // pega a string do CSV

        if (string.IsNullOrWhiteSpace(csvValue))
            return new List<ServicoModel>();

        // Supondo que os serviços no CSV venham separados por vírgula
        var nomesServicos = csvValue
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Trim());

        // Converter para objetos ServicoModel
        var listaServicos = nomesServicos
            .Select(n => new ServicoModel { Servico = n })
            .ToList();

        return listaServicos;
    });
    }
}
//TODO validar certinho essa parte da importação
using System.ComponentModel.DataAnnotations;

namespace LiveNet.Api.ViewModels;

public class ContatoViewModel
{
    public Guid Id { get; set; }

    [StringLength(100)]
    public string? Nome { get; set; }

    [StringLength(50)]
    [EmailAddress]
    public string? Cpf { get; set; }
    public string? EmailEmpresa { get; set; }

    [StringLength(50)]
    [EmailAddress]
    public string? EmailPessoal { get; set; }

    [StringLength(11)]
    public string? Telefone { get; set; }
    public string? Empresa { get; set; }

    [StringLength(30)]
    public string? Cargo { get; set; }
    public bool Cliente { get; set; }
    public List<string>? Interesses { get; set; }
    public string? ModoInclusao { get; set; }
    public List<ServicoViewModel>? Servicos { get; set; }
}

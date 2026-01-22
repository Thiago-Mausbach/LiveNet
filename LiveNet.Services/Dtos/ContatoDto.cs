using System.ComponentModel.DataAnnotations;

namespace LiveNet.Services.Dtos;

public class ContatoDto
{
    public Guid Id { get; set; }

    [StringLength( 100 )]
    public string? Nome { get; set; }

    [StringLength( 11 )]
    public string? Cpf { get; set; }

    [StringLength( 50 )]
    [EmailAddress]
    public string? EmailEmpresa { get; set; }

    [StringLength( 50 )]
    [EmailAddress]
    public string? EmailPessoal { get; set; }

    [StringLength( 11 )]
    public string? Telefone { get; set; }
    [StringLength( 50 )]
    public string? Empresa { get; set; }
    [StringLength( 14 )]
    public string? CnpjEmpresa { get; set; }

    [StringLength( 30 )]
    public string? Cargo { get; set; }
    public bool Cliente { get; set; }
    public bool IsFavorito { get; set; }
    public ICollection<InteresseDto>? Interesses { get; set; }
    public string? ModoInclusao { get; set; }
    public ICollection<ServicoDto>? Servicos { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace LiveNet.Services.Dtos;

public class ServicoDto
{
    public Guid Id { get; set; }
    [Required]
    [StringLength( 30 )]
    public string Servico { get; set; } = null!;
}
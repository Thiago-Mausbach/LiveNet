using System.ComponentModel.DataAnnotations;

namespace LiveNet.Services.Dtos;

public class UsuarioDto
{
    public Guid Id { get; set; }
    [StringLength( 45 )]
    public required string Nome { get; set; }
    [StringLength( 40 )]
    public required string Email { get; set; }
    [MinLength( 8 )]
    public required string Senha { get; set; }
}
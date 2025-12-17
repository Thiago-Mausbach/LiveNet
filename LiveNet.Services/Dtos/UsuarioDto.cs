namespace LiveNet.Services.Dtos;

public class UsuarioDto
{
    public Guid Id { get; set; }
    public required string Nome { get; set; }
    public required string Email { get; set; }
    public string Senha { get; set; }
}
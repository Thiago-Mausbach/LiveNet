namespace LiveNet.Domain.Models;

public class FavoritosModel
{
    public Guid UsuarioId { get; set; }
    public Guid ContatoId { get; set; }
    public UsuarioModel Usuario { get; set; } = null!;
    public ContatoModel Contato { get; set; } = null!;
}
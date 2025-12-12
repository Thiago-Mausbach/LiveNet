namespace LiveNet.Domain.ViewModels
{
    public class UsuarioViewModel
    {
        public Guid Id { get; set; }
        public required string Nome { get; set; }
        public required string Email { get; set; }
    }
}

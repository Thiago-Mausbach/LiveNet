using System.ComponentModel.DataAnnotations;

namespace LiveNet.Api.ViewModels
{
    public class UsuarioViewModel
    {
        public Guid Id { get; set; }
        [StringLength(40)]
        public required string Nome { get; set; }
        [StringLength(30)]
        public required string Email { get; set; }
        [MinLength(8)]
        public required string Senha { get; set; }
    }
}

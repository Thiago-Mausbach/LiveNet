using System.ComponentModel.DataAnnotations;

namespace LiveNet.Api.ViewModels
{
    public class ServicoViewModel
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(30)]
        public string Servico { get; set; } = null!;
    }
}

using System.ComponentModel.DataAnnotations;

namespace LiveNet.Api.ViewModels
{
    public class ServicoViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string Servico { get; set; } = null!;
    }
}

using System.ComponentModel.DataAnnotations;

namespace LiveNet.Api.ViewModels;

public class EmpresaViewModel
{
    public Guid Id { get; set; }
    [Required]
    [StringLength(14)]
    public string Cnpj { get; set; } = null!;
    [Required]
    [StringLength(40)]
    public string RazaoSocial { get; set; } = null!;
}

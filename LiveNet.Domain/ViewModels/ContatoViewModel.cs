namespace LiveNet.Domain.ViewModels;

public class ContatoViewModel
{
    public string Nome { get; set; } = null!;
    public string EmailEmpresa { get; set; } = null!;
    public string EmailPessoal { get; set; } = null!;
    public string Telefone { get; set; } = null!;
    public string Empresa { get; set; } = null!;
    public string Cargo { get; set; } = null!;
    public bool Cliente { get; set; }
    public List<string> Interesses { get; set; } = null!;
    public List<ServicoViewModel> Servicos { get; set; } = null!;
    public string ModoInclusao { get; set; } = null!;
}

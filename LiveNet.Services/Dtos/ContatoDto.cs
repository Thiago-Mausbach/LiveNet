namespace LiveNet.Services.Dtos;

public class ContatoDto
{
    public Guid Id { get; set; }
    public string? Nome { get; set; }
    public string? Cpf { get; set; }
    public string? EmailEmpresa { get; set; }
    public string? EmailPessoal { get; set; }
    public string? Telefone { get; set; }
    public string? Empresa { get; set; }
    public string? CnpjEmpresa { get; set; }
    public string? Cargo { get; set; }
    public bool Cliente { get; set; }
    public bool IsFavorito { get; set; }
    public ICollection<InteresseDto>? Interesses { get; set; }
    public string? ModoInclusao { get; set; }
    public ICollection<ServicoDto>? Servicos { get; set; }
}
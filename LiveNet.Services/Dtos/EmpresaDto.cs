namespace LiveNet.Services.Dtos;

public class EmpresaDto
{
    public Guid Id { get; set; }
    public string Cnpj { get; set; } = null!;
    public string RazaoSocial { get; set; } = null!;
}
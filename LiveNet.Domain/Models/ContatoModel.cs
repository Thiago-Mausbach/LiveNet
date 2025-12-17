using LiveNet.Domain.Interfaces;

namespace LiveNet.Domain.Models;

public class ContatoModel : IAuditColumns, ISoftDelete
{
    public Guid Id { get; set; }
    public string? Nome { get; set; }
    public string? Cpf { get; set; }
    public string? EmailEmpresa { get; set; }
    public string? EmailPessoal { get; set; }
    public string? Telefone { get; set; }
    public string? CnpjEmpresa { get; set; }
    public string? Cargo { get; set; }
    public bool Cliente { get; set; }
    public ICollection<ContatoInteresseModel>? Interesses { get; set; }
    public string? ModoInclusao { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    public Guid? DeletedBy { get; set; }
    public bool IsDeleted { get; set; }
    public ICollection<ContatoServicoModel>? Servicos { get; set; }
    public EmpresaModel? Empresa { get; set; }
}
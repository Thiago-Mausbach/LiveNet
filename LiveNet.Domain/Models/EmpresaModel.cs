using LiveNet.Domain.Interfaces;

namespace LiveNet.Domain.Models;

public class EmpresaModel : IAuditColumns, ISoftDelete
{
    public Guid Id { get; set; }
    public string Cnpj { get; set; } = null!;
    public string RazaoSocial { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    public Guid? DeletedBy { get; set; }
    public bool IsDeleted { get; set; }
}


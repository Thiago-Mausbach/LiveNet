namespace LiveNet.Domain.Interfaces;

public interface IAuditColumns
{
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}

namespace LiveNet.Domain.Models
{
    public class ServicoModel
    {
        public Guid Id { get; set; }
        public required string Servico { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
        public Guid? DeletedBy { get; set; }
        public bool IsDeleted { get; set; }

        /*      NA,
                Gestor,
                LivesignSemGuarda,
                LivesignComGuarda,
                Livework,
                Liveregister,
                Livemail*/
    }
}

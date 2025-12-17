using System.ComponentModel.DataAnnotations;

namespace LiveNet.Domain.Models
{
    public class InteresseModel
    {
        public Guid Id { get; set; }

        [StringLength(30)]
        public required string Interesse { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
        public Guid? DeletedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}

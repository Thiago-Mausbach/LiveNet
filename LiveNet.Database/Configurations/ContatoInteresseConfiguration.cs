using LiveNet.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LiveNet.Database.Configurations;

public class ContatoInteresseConfiguration : IEntityTypeConfiguration<ContatoInteresseModel>
{
    public void Configure(EntityTypeBuilder<ContatoInteresseModel> entity)
    {
        entity.HasKey(x => new
        {
            x.ContatoId,
            x.InteresseId
        });

        entity.HasOne(x => x.Contato)
              .WithMany(c => c.Interesses)
              .HasForeignKey(x => x.ContatoId);

        entity.HasOne(x => x.Interesse)
                          .WithMany()
                          .HasForeignKey(x => x.InteresseId);
    }
}
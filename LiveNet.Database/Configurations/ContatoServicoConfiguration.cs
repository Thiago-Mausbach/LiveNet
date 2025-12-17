using LiveNet.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LiveNet.Database.Configurations;

public class ContatoServicoConfiguration
    : IEntityTypeConfiguration<ContatoServicoModel>
{
    public void Configure(EntityTypeBuilder<ContatoServicoModel> entity)
    {
        entity.HasKey(x => new
        {
            x.ContatoId,
            x.ServicoId
        });

        entity.HasOne(x => x.Contato)
               .WithMany(c => c.Servicos)
               .HasForeignKey(x => x.ContatoId);

        entity.HasOne(x => x.Servico)
               .WithMany()
               .HasForeignKey(x => x.ServicoId);
    }
}
using LiveNet.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LiveNet.Database.Configurations;

public class ServicoConfiguration
    : IEntityTypeConfiguration<ServicoModel>
{
    public void Configure(EntityTypeBuilder<ServicoModel> entity)
    {
        entity.HasKey(x => x.Id);

        entity.Property(x => x.Servico).HasMaxLength(30);
    }
}
using LiveNet.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LiveNet.Database.Configurations;

public class EmpresaConfiguration
    : IEntityTypeConfiguration<EmpresaModel>
{
    public void Configure(EntityTypeBuilder<EmpresaModel> entity)
    {
        entity.HasKey(x => x.Id);

        entity.HasIndex(e => e.Cnpj).IsUnique();

        entity.Property(e => e.Cnpj)
              .IsRequired()
              .HasMaxLength(14)
              .IsFixedLength(true);

        entity.Property(e => e.RazaoSocial)
              .IsRequired();

        entity.ToTable(t => t.HasCheckConstraint("CK_Contato_Cnpj ", "Cnpj NOT LIKE '%[0-9]%' "));
    }
}
using LiveNet.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LiveNet.Database.Configurations;

public class UsuarioConfiguration
    : IEntityTypeConfiguration<UsuarioModel>
{
    public void Configure(EntityTypeBuilder<UsuarioModel> entity)
    {
        entity.HasKey(x => x.Id);

        entity.Property(x => x.Nome).IsRequired().HasMaxLength(45);

        entity.HasIndex(x => x.Email).IsUnique();

        entity.ToTable(t => t.HasCheckConstraint("CK_Usuario_Senha", "LEN(Senha) BETWEEN 8 AND 60"));

    }
}
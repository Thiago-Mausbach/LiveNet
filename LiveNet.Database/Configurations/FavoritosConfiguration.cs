using LiveNet.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LiveNet.Database.Configurations;

public class FavoritosConfiguration
    : IEntityTypeConfiguration<FavoritosModel>
{
    public void Configure(EntityTypeBuilder<FavoritosModel> entity)
    {
        entity.HasKey(x => new { x.UsuarioId, x.ContatoId });

        entity.HasOne(x => x.Usuario)
              .WithMany()
              .HasForeignKey(x => x.UsuarioId);

        entity.HasOne(x => x.Contato)
              .WithMany()
              .HasForeignKey(x => x.ContatoId);
    }
}

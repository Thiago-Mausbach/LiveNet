using LiveNet.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LiveNet.Database.Configurations;

public class ContatoConfiguration : IEntityTypeConfiguration<ContatoModel>
{
    public void Configure(EntityTypeBuilder<ContatoModel> entity)
    {
        #region Configuração das propriedades
        entity.HasKey(x => x.Id);

        entity.Property(x => x.Nome)
               .IsRequired()
               .HasMaxLength(50);

        entity.Property(x => x.Cpf).HasMaxLength(11).IsFixedLength(true);

        entity.HasOne(x => x.Empresa)
               .WithMany()
               .HasForeignKey(x => x.Id)
               .OnDelete(DeleteBehavior.SetNull);

        entity.HasIndex(x => x.EmailEmpresa).IsUnique();

        entity.HasIndex(x => x.EmailPessoal).IsUnique();

        entity.Property(c => c.EmailPessoal)
       .HasConversion(
           v => v.ToLower(),
           v => v
       );

        entity.Property(c => c.EmailEmpresa)
.HasConversion(
   v => v.ToLower(),
   v => v
);

        entity.Property(x => x.EmailEmpresa).HasMaxLength(45);

        entity.Property(x => x.EmailPessoal).HasMaxLength(45);

        entity.Property(x => x.Telefone).HasMaxLength(11).IsFixedLength(true);

        entity.Property(x => x.Cargo).HasMaxLength(50);

        #endregion

        entity.ToTable(e => e.HasCheckConstraint("CK_Contato_Cpf ", "Cpf NOT LIKE '%[0-9]%' "));
        entity.ToTable(e => e.HasCheckConstraint("CK_Contato_Telefone ", "Telefone NOT LIKE '%[0-9]%' "));

    }
}

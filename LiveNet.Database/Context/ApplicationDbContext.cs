using LiveNet.Domain.Interfaces;
using LiveNet.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Linq.Expressions;

namespace LiveNet.Database.Context;
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<ContatoInteresseModel> ContatoInteresse { get; set; }
    public DbSet<ContatoModel> Contatos { get; set; }
    public DbSet<ContatoServicoModel> ContatoServico { get; set; }
    public DbSet<EmpresaModel> Empresas { get; set; }
    public DbSet<FavoritosModel> Favoritos { get; set; }
    public DbSet<InteresseModel> Interesses { get; set; }
    public DbSet<ServicoModel> Servicos { get; set; }
    public DbSet<UsuarioModel> Usuarios { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        #region Seta valor de CreatedAt e UpdatedAt automaticamnete

        var auditColumnsEntities = typeof(IAuditColumns).Assembly.GetTypes()
            .Where(type => typeof(IAuditColumns)
                            .IsAssignableFrom(type)
                            && type.IsClass
                            && !type.IsAbstract).ToList();

        foreach (var entry in ChangeTracker.Entries().Where(entry => auditColumnsEntities.Contains(entry.Entity.GetType())))
        {
            if (entry.State == EntityState.Added)
            {
                // Lógica para inserção
                entry.Property(nameof(IAuditColumns.CreatedAt)).CurrentValue = DateTimeOffset.Now;
            }

            if (entry.State == EntityState.Modified)
            {
                // Lógica para atualização
                entry.Property(nameof(IAuditColumns.UpdatedAt)).CurrentValue = DateTimeOffset.Now;
            }
        }

        #endregion Seta valor de CreatedAt e UpdatedAt automaticamnete

        return base.SaveChangesAsync(cancellationToken);

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Override que remove itens com "IsDeleted" sendo 'true' de listangens
        modelBuilder.Entity<ContatoModel>().HasQueryFilter(l => !l.IsDeleted);

        //Builder Favoritos
        modelBuilder.Entity<FavoritosModel>(entity =>
        {
            entity.HasKey(x => new { x.UsuarioId, x.ContatoId });

            entity.HasOne(x => x.Usuario)
                  .WithMany()
                  .HasForeignKey(x => x.UsuarioId);

            entity.HasOne(x => x.Contato)
                  .WithMany()
                  .HasForeignKey(x => x.ContatoId);
        });

        //Builder Empresa
        modelBuilder.Entity<EmpresaModel>(entity =>
        {
            entity.HasIndex(e => e.Cnpj).IsUnique();

            entity.Property(e => e.Cnpj)
                  .IsRequired();

            entity.Property(e => e.RazaoSocial)
                  .IsRequired();
        });


        //Builder Contato
        modelBuilder.Entity<ContatoModel>(entity =>
        {
            entity.HasOne(c => c.Empresa)
                  .WithMany()
                  .HasForeignKey(c => c.CnpjEmpresa)
                  .OnDelete(DeleteBehavior.SetNull);
        });

        //Builder do relacionamento Contato - Interesse (N-N)
        modelBuilder.Entity<ContatoInteresseModel>(entity =>
        {
            entity.HasKey(x => new { x.ContatoId, x.InteresseId });

            entity.HasOne(x => x.Contato)
                  .WithMany(c => c.Interesses)
                  .HasForeignKey(x => x.ContatoId);

            entity.HasOne(x => x.Interesse)
                  .WithMany()
                  .HasForeignKey(x => x.InteresseId);
        });

        modelBuilder.Entity<ContatoServicoModel>(entity =>
        {
            entity.HasKey(x => new { x.ContatoId, x.ServicoId });

            entity.HasOne(x => x.Contato)
                  .WithMany(c => c.Servicos)
                  .HasForeignKey(x => x.ContatoId);

            entity.HasOne(x => x.Servico)
                  .WithMany()
                  .HasForeignKey(x => x.ServicoId);
        });


        var softDeleteEntities = typeof(ISoftDelete).Assembly.GetTypes()
    .Where(type => typeof(ISoftDelete)
                    .IsAssignableFrom(type)
                    && type.IsClass
                    && !type.IsAbstract);

        foreach (var softDeleteEntity in softDeleteEntities)
        {
            modelBuilder.Entity(softDeleteEntity).HasQueryFilter(
                  GenerateQueryFilterLambda(softDeleteEntity));

            modelBuilder.Entity(softDeleteEntity).HasIndex(nameof(ISoftDelete.IsDeleted));
        }

        base.OnModelCreating(modelBuilder);
    }

    private static LambdaExpression? GenerateQueryFilterLambda(Type type)
    {
        var parameter = Expression.Parameter(type, "w");
        var falseConstantValue = Expression.Constant(false);
        var propertyAccess = Expression.PropertyOrField(parameter, nameof(ISoftDelete.IsDeleted));
        var equalExpression = Expression.Equal(propertyAccess, falseConstantValue);
        var lambda = Expression.Lambda(equalExpression, parameter);

        return lambda;
    }
}

public class AppDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}

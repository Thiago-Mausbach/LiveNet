using LiveNet.Domain.Interfaces;
using LiveNet.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Linq.Expressions;

namespace LiveNet.Database.Context;
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<ContatoModel> Contato { get; set; }
    public DbSet<EmpresaModel> Empresa { get; set; }
    public DbSet<FavoritosModel> Favoritos { get; set; }
    public DbSet<ServicoModel> Servico { get; set; }
    public DbSet<UsuarioModel> Usuario { get; set; }
    public DbSet<UsuarioServicoModel> UsuarioServico { get; set; }

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
                  .HasMaxLength(14)
                  .IsRequired();

            entity.Property(e => e.RazaoSocial)
                  .HasMaxLength(150)
                  .IsRequired();
        });


        //Builder Contato
        modelBuilder.Entity<ContatoModel>(entity =>
        {
            entity.HasOne(c => c.Empresa)
                  .WithMany()
                  .HasForeignKey(c => c.EmpresaId)
                  .OnDelete(DeleteBehavior.SetNull);
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

        //Converte os interesses adicionados em uma lista de string
        modelBuilder.Entity<ContatoModel>()
        .Property(c => c.Interesses)
        .HasConversion(
         v => string.Join(",", v),
        v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
          .Select(x => x.Trim()) // remove espaços extras
          .ToList()
);

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

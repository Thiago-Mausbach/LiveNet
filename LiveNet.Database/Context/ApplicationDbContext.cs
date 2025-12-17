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
        modelBuilder.Entity<EmpresaModel>().HasQueryFilter(l => !l.IsDeleted);
        modelBuilder.Entity<InteresseModel>().HasQueryFilter(l => !l.IsDeleted);
        modelBuilder.Entity<ServicoModel>().HasQueryFilter(l => !l.IsDeleted);
        modelBuilder.Entity<UsuarioModel>().HasQueryFilter(l => !l.IsDeleted);

        //Aplica as configurações realizadas no Database.Configurations
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

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

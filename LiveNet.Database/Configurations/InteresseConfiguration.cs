using LiveNet.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LiveNet.Database.Configurations;

public class InteresseConfiguration
    : IEntityTypeConfiguration<InteresseModel>
{
    public void Configure(EntityTypeBuilder<InteresseModel> entity)
    {
        entity.HasKey(x => x.Id);

        entity.Property(x => x.Interesse).IsRequired().HasMaxLength(35);
    }
}
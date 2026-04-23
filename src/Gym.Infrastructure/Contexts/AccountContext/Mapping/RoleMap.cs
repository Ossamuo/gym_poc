using Gym.Domain.Contexts.AccountContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gym.Infrastructure.Contexts.AccountContext.Mapping;

public class RoleMap : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");
        builder.HasKey(x => x.Id);
        
        builder.Property(x=> x.Name)
            .HasColumnName("Name")
            .HasColumnType("varchar")
            .HasMaxLength(128)
            .IsRequired();
        
    }
}
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gym.Domain.Contexts.PartnerContext.Entities;

namespace Gym.Infrastructure.Contexts.PartnerContext.Mapping
{
    public class PartnerMap : IEntityTypeConfiguration<Partner>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Partner> builder)
        {
            builder
                .ToTable("Partners");

            builder
                .HasKey(x => x.Id)
                .HasName("PK_Partners");

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.ApiKey)
                //.IsRequired()
                .HasMaxLength(50);

            builder.OwnsOne(x => x.ApiSecretHash, password =>
            {
                password.Property(p => p.Hash)
                    .HasColumnName("ApiSecretHash")
                    .HasMaxLength(500)
                    .HasField("<Hash>k__BackingField")
                    .UsePropertyAccessMode(PropertyAccessMode.Field);
            });
        }
    }
}

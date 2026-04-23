using Gym.Domain.Contexts.AccountContext.Entities;
using Gym.Domain.Contexts.ActivitiesContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Infrastructure.Contexts.ActivitiesContext.Mapping
{
    public class ActivityMap : IEntityTypeConfiguration<Activity>
    {
        public void Configure(EntityTypeBuilder<Activity> builder)
        {
            builder
            .ToTable("Activities");


            builder
                .HasKey(x => x.Id)
                .HasName("PK_Activities");

            builder.Property(x => x.Name)
                .HasColumnName("Name")
                .HasMaxLength(120)
                .HasColumnType("nvarchar")
                .IsRequired();

            builder.Property(x => x.Description)
                .HasColumnName("Description")
                .HasMaxLength(500)
                .HasColumnType("nvarchar")
                .IsRequired();

            builder.Property(x => x.ImageUrl)
                .HasColumnName("ImageUrl")
                .HasMaxLength(200)
                .HasColumnType("nvarchar")
                .IsRequired();

            builder.Property(x => x.StartAdt)
                .HasColumnName("StartAdt")
                .HasColumnType("datetime2")
                .IsRequired();

            builder.Property(x => x.EndAdt)
                .HasColumnName("EndAdt")
                .HasColumnType("datetime2")
                .IsRequired();
            
        }
    }
}

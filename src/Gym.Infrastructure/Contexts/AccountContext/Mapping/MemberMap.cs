using Gym.Domain.Contexts.AccountContext.Entities;
using Gym.Domain.Contexts.AccountContext.ValueObjects;
using Gym.Domain.Contexts.ActivitiesContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gym.Infrastructure.Contexts.AccountContext.Mapping;


public class MemberMap : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder
            .ToTable("Members");


        builder
            .HasKey(x => x.Id)
            .HasName("PK_Members");


        builder.Property(x => x.Name)
            .HasColumnName("Name")
            .HasMaxLength(120)
            .HasColumnType("nvarchar")
            .IsRequired();

        builder.OwnsOne(x => x.Email)
            .Property(x => x.Address)
            .HasColumnName("Email")
            .HasMaxLength(Email.MaxLength)
            .IsRequired();

        builder.OwnsOne(x => x.Email)
            .Property(x => x.Hash)
            .HasColumnName("EmailHash")
            .HasMaxLength(Email.MaxLength)
            .IsRequired();

        builder.OwnsOne(x => x.Email)
            .OwnsOne(x => x.Verification)
            .Property(x => x.Code)
            .HasColumnName("EmailVerificationCode")
            .IsRequired(false);

        builder.OwnsOne(x => x.Email)
            .OwnsOne(x => x.Verification)
            .Property(x => x.ExpiresAt)
            .HasColumnName("EmailVerificationExpiresAt")
            .IsRequired(false);

        builder.OwnsOne(x => x.Email)
            .OwnsOne(x => x.Verification)
            .Property(x => x.VerifiedAt)
            .HasColumnName("EmailVerificationVerifiedAt")
            .IsRequired(false);

        builder.OwnsOne(x => x.Email)
            .OwnsOne(x => x.Verification)
            .Ignore(x => x.IsActive);

        builder.OwnsOne(x => x.Password)
            .Property(x => x.Hash)
            .HasColumnName("PasswordHash")
            .IsRequired();

        builder.OwnsOne(x => x.Password)
            .Property(x => x.ResetCode)
            .HasColumnName("PasswordResetCode")
            .IsRequired();

        ConfigureMemberRoles(builder);
    }

    private void ConfigureMemberRoles(EntityTypeBuilder<Member> builder)
    {
        builder
            .HasMany(x => x.Roles)
            .WithMany(x => x.Members)
            .UsingEntity<Dictionary<string, object>>(
                "MemberRoles",
                role => role
                    .HasOne<Role>().WithMany()
                    .HasForeignKey("RoleId")
                    .OnDelete(DeleteBehavior.Cascade),
                member => member
                    .HasOne<Member>()
                    .WithMany()
                    .HasForeignKey("MemberId")
                    .OnDelete(DeleteBehavior.Cascade)
            );
    }


}
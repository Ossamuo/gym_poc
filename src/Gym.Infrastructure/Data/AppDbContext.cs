using Gym.Domain.Contexts.AccountContext.Entities;
using Gym.Domain.Contexts.ActivitiesContext.Entities;
using Gym.Domain.Contexts.PartnerContext.Entities;
using Gym.Infrastructure.Contexts.AccountContext.Mapping;
using Gym.Infrastructure.Contexts.ActivitiesContext.Mapping;
using Gym.Infrastructure.Contexts.PartnerContext.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Gym.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Member> Members { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;
    public DbSet<Partner> Partners { get; set; } = null!;

    public DbSet<Activity> Activities { get; set; } = null!;
    public DbSet<Booking> Bookings { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new MemberMap());
        modelBuilder.ApplyConfiguration(new RoleMap());
        modelBuilder.ApplyConfiguration(new ActivityMap());
        modelBuilder.ApplyConfiguration(new PartnerMap());
        modelBuilder.ApplyConfiguration(new BookingMap());
    }
}
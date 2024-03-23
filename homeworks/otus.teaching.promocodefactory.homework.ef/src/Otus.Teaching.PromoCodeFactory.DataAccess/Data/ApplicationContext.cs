using Microsoft.EntityFrameworkCore;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;

namespace Otus.Teaching.PromoCodeFactory.DataAccess.Data;

public class ApplicationContext(DbContextOptions<ApplicationContext> options) : DbContext(options)
{
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<PromoCode> PromoCodes => Set<PromoCode>();
    public DbSet<Preference> Preferences => Set<Preference>();


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.EnableSensitiveDataLogging();
        // optionsBuilder.UseSqlite("Data Source=Employees.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomerPreference>(entity => entity.HasKey(e => new { e.CustomerId, e.PreferenceId }));

        modelBuilder.Entity<CustomerPreference>()
            .HasOne(bc => bc.Customer)
            .WithMany(b => b.Preferences)
            .HasForeignKey(bc => bc.CustomerId);
        modelBuilder.Entity<CustomerPreference>()
            .HasOne(bc => bc.Preference)
            .WithMany()
            .HasForeignKey(bc => bc.PreferenceId);

        modelBuilder.Entity<PromoCode>()
            .HasOne(u => u.Customer)
            .WithMany(c => c.PromoCodes)
            .OnDelete(DeleteBehavior.Cascade);
        
    }
}
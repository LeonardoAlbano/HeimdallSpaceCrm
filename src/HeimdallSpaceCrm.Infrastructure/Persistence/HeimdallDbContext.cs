using HeimdallSpaceCrm.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace HeimdallSpaceCrm.Infrastructure.Persistence;

public class HeimdallDbContext : DbContext
{
    public HeimdallDbContext(DbContextOptions<HeimdallDbContext> options)
    {
    }
    
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.HasIndex(x => x.Email).IsUnique();
            entity.Property(x => x.Name).IsRequired().HasMaxLength(200);
            entity.Property(x => x.Email).IsRequired().HasMaxLength(200);
            entity.Property(x => x.PasswordHash).IsRequired();
        });
    }
}
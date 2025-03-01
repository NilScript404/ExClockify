using Microsoft.EntityFrameworkCore;

namespace ExClockify.Models;

public class ExClockifyContext : DbContext
{
    public ExClockifyContext(DbContextOptions<ExClockifyContext> options)
        : base(options) { }
    
    public DbSet<Track> Tracks { get; set; } = null!;
    public DbSet<User> Users {get; set;} = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasKey(user => user.deviceId);
        
        modelBuilder.Entity<Track>()
            .HasOne(track => track.user)
            .WithMany(user => user.Tracks)
            .HasForeignKey(track => track.UserDeviceId);
        
        base.OnModelCreating(modelBuilder);
    }
}

using Microsoft.EntityFrameworkCore;

namespace ExClockify.Models;

public class ExClockifyContext : DbContext
{
    public ExClockifyContext(DbContextOptions<ExClockifyContext> options)
        : base(options) { }
    
    public DbSet<Track> Tracks { get; set; } = null!;
    public DbSet<User> Users {get; set;} = null!;
    
    // fluent api is not really needed here, it does the samething as the conventions
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasKey(user => user.deviceId);
        
        modelBuilder.Entity<Track>()
            .HasOne(track => track.User)
            .WithMany(user => user.Tracks)
            .HasForeignKey(track => track.UserDeviceId);
        
        base.OnModelCreating(modelBuilder);
    }
}

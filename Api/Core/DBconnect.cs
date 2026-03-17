using Api.Models;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Events> Events { get; set; }
    public DbSet<Booking> Bookings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Ограничение повторного бронирования
        modelBuilder.Entity<Booking>()
            .HasIndex(b => new { b.event_id, b.user_id })
            .IsUnique();
    }
}
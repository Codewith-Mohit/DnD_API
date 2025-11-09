using DnD_API.Models;
using Microsoft.EntityFrameworkCore;

namespace DnD_API.Data
{
    public class DnDDbContext : DbContext
    {
        public DnDDbContext(DbContextOptions<DnDDbContext> options) : base(options) { } 
        public DbSet<Character> Characters { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Run> Runs { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Enemy> Enemies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Run.Log is a collection of RunLogEntry (DiceRollResult owned)
            modelBuilder.Entity<Run>(rb =>
            {
                rb.OwnsMany(r => r.Log, lb => lb.WithOwner().HasForeignKey("RunId"));                
            });

            // Room.Encounter with Enemies as owned collection
            modelBuilder.Entity<Room>(rb =>
            {
                rb.OwnsOne(r => r.Encounter, eb => eb.OwnsMany(e => e.Enemies));
            });

            // Basic key for Item
            modelBuilder.Entity<Item>().HasKey(i => i.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}

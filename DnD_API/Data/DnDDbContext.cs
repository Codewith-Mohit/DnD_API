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
            modelBuilder.Owned<DiceRollResult>();
            
            modelBuilder.Entity<Run>(rb =>
            {
                rb.OwnsMany(r => r.Log, lb => lb.WithOwner().HasForeignKey("RunId"));                
            });

            modelBuilder.Entity<Room>(rb =>
            {
                rb.OwnsOne(r => r.Encounter, eb => eb.OwnsMany(e => e.Enemies));
            });
            
            modelBuilder.Entity<Item>().HasKey(i => i.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}

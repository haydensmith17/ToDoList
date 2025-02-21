using Microsoft.EntityFrameworkCore;
using Backend.Models;

namespace Backend
{
    public class DbSource : DbContext
    {
        public DbSource(DbContextOptions<DbSource> options) : base(options)
        {
        }

        public virtual DbSet<ToDoItem> ToDoItem { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDoItem>(entity => {
                entity.HasKey(e => e.Id);
                entity.ToTable("todo");
                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
                entity.Property(e => e.Item).HasColumnName("item").HasMaxLength(255);
                entity.Property(e => e.IsComplete).HasColumnName("iscomplete");
                entity.Property(e => e.Created).HasColumnName("created");
            });
        }
    }
}
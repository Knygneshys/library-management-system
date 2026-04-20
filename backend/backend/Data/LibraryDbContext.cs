using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data;

public class LibraryDbContext(DbContextOptions<LibraryDbContext> options) : DbContext(options)
{
        public DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
                modelBuilder.Entity<Author>()
                        .HasIndex(a => a.FullName)
                        .IsUnique();
        }
}
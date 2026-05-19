using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data;

public class LibraryDbContext(DbContextOptions<LibraryDbContext> options) : DbContext(options)
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<ParcelLocker> ParcelLockers { get; set; }
    public DbSet<Locker> Lockers { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<PrintingHouse> PrintingHouses { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Publisher> Publishers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>()
                .HasIndex(a => a.FullName)
                .IsUnique();

        modelBuilder.Entity<ParcelLocker>()
                .HasIndex(p => p.Address)
                .IsUnique();

        modelBuilder.Entity<Locker>()
                .HasIndex(p => p.LocationCode)
                .IsUnique();

        modelBuilder.Entity<Locker>()
                .HasOne(b => b.ParcelLocker)
                .WithMany(a => a.Lockers)
                .HasForeignKey(b => b.ParcelLockerId)
                .IsRequired();

        modelBuilder.Entity<Book>()
                .HasIndex(b => b.Isbn)
                .IsUnique();

        modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId)
                .IsRequired();

        modelBuilder.Entity<Book>()
                .HasOne(b => b.PrintingHouse)
                .WithMany(ph => ph.Books)
                .HasForeignKey(b => b.PrintingHouseId)
                .IsRequired();

        modelBuilder.Entity<Book>()
                .HasOne(b => b.Publisher)
                .WithMany(p => p.Books)
                .HasForeignKey(b => b.PublisherId)
                .IsRequired();

        modelBuilder.Entity<PrintingHouse>()
                .HasIndex(a => a.Name)
                .IsUnique();

        modelBuilder.Entity<Genre>()
                .HasIndex(g => g.Title)
                .IsUnique();

        modelBuilder.Entity<Publisher>()
                .HasIndex(p => p.Name)
                .IsUnique();

        modelBuilder.Entity<Book>()
                .HasMany(b => b.Genres)
                .WithMany(g => g.Books)
                .UsingEntity(join => join.ToTable("BookGenres"));
    }
}
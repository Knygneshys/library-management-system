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
    public DbSet<Copy> Copies { get; set; }
    public DbSet<Loan> Loans { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<IssueCompartment> IssueCompartments { get; set; }
    public DbSet<LibrarianTask> LibrarianTasks { get; set; }

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

        // Book - Copy (1 to 0..*)
        modelBuilder.Entity<Copy>()
                .HasOne(c => c.Book)
                .WithMany(b => b.Copies)
                .HasForeignKey(c => c.BookId)
                .IsRequired();

        // Copy - Loan (1 to 0..1): Loan requires a Copy (1), Copy can have 0..1 Loan
        modelBuilder.Entity<Loan>()
                .HasOne(l => l.Copy)
                .WithOne(c => c.Loan)
                .HasForeignKey<Loan>(l => l.CopyId)
                .IsRequired();

        // Loan - Reservation (0..1 to 1): Loan must have Reservation (1), Reservation can have 0..1 Loan
        modelBuilder.Entity<Loan>()
                .HasOne(l => l.Reservation)
                .WithOne(r => r.Loan)
                .HasForeignKey<Loan>(l => l.ReservationId)
                .IsRequired();

        // Book - Reservation (1 to 0..*)
        modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Book)
                .WithMany(b => b.Reservations)
                .HasForeignKey(r => r.BookId)
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
        
        modelBuilder.Entity<IssueCompartment>()
                .HasOne(ic => ic.Locker)
                .WithMany(l => l.IssueCompartments)
                .HasForeignKey(ic => ic.LockerId)
                .IsRequired();
        
        modelBuilder.Entity<IssueCompartment>()
                .HasOne(ic => ic.Reservation)
                .WithOne(r => r.IssueCompartment)
                .HasForeignKey<IssueCompartment>(ic => ic.ReservationId)
                .IsRequired();
        
        modelBuilder.Entity<LibrarianTask>()
                .HasOne(t => t.Reservation)
                .WithOne(r => r.LibrarianTask)
                .HasForeignKey<LibrarianTask>(t => t.ReservationId)
                .IsRequired();
    }
}
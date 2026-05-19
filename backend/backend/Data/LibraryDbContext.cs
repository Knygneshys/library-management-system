using backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection.Emit;
using static System.Net.WebRequestMethods;

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


        modelBuilder.Entity<IssueCompartment>()
                .HasOne(i => i.Locker)
                .WithOne(l => l.IssueCompartment)
                .HasForeignKey<IssueCompartment>(i => i.LockerId)
                .IsRequired();

        modelBuilder.Entity<Reservation>()
                .HasOne(r => r.IssueCompartment)
                .WithOne(i => i.IssueReservation)
                .HasForeignKey<Reservation>(r => r.IssueCompartmentId)
                .IsRequired(false); 

        modelBuilder.Entity<Reservation>()
                .HasOne(r => r.ReturnCompartment)
                .WithOne(i => i.ReturnReservation)
                .HasForeignKey<Reservation>(r => r.ReturnCompartmentId)
                .IsRequired(false);

        modelBuilder.Entity<Loan>()
                .HasOne(l => l.Copy)
                .WithOne(c => c.Loan)
                .HasForeignKey<Loan>(l => l.CopyId)
                .IsRequired();

        modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Copy)
                .WithOne(c => c.Reservation)
                .HasForeignKey<Reservation>(r => r.CopyId)
                .IsRequired(false);

        modelBuilder.Entity<Loan>()
                .HasOne(l => l.User)
                .WithMany(u => u.Loans)
                .HasForeignKey(l => l.UserId)
                .IsRequired();

        modelBuilder.Entity<LibraryTask>()
                .HasOne(t => t.Reservation)
                .WithMany(r => r.LibraryTasks)
                .HasForeignKey(t => t.ReservationId)
                .IsRequired();

    }
}
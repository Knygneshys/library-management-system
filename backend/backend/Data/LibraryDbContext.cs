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
    public DbSet<IssueCompartment> IssueCompartments { get; set; }
    public DbSet<LibraryTask> LibraryTasks { get; set; }
    public DbSet<User> Users { get; set; }

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

        modelBuilder.Entity<Copy>()
                .HasOne(c => c.Book)
                .WithMany(b => b.Copies)
                .HasForeignKey(c => c.BookId)
                .IsRequired();

        modelBuilder.Entity<Loan>()
                .HasOne(l => l.Copy)
                .WithOne(c => c.Loan)
                .HasForeignKey<Loan>(l => l.CopyId)
                .IsRequired();

        modelBuilder.Entity<Loan>()
                .HasOne(l => l.Reservation)
                .WithOne(r => r.Loan)
                .HasForeignKey<Loan>(l => l.ReservationId)
                .IsRequired();

        modelBuilder.Entity<Loan>()
                .HasOne(r => r.User)
                .WithMany(u => u.Loans)
                .HasForeignKey(r => r.UserId)
                .IsRequired();

        modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Book)
                .WithMany(b => b.Reservations)
                .HasForeignKey(r => r.BookId)
                .IsRequired();

        modelBuilder.Entity<Reservation>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.UserId)
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


        modelBuilder.Entity<LibraryTask>()
                .HasOne(t => t.Reservation)
                .WithMany(r => r.LibraryTasks)
                .HasForeignKey(t => t.ReservationId)
                .IsRequired();

    }
}
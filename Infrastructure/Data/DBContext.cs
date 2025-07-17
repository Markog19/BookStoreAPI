using BookStoreAPI.Domain.Entities;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

public class DBContext : DbContext
{
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Author> Authors => Set<Author>();
    public DbSet<Genre> Genres => Set<Genre>();
    public DbSet<Review> Reviews => Set<Review>();
    public DbSet<BookAuthor> BookAuthors => Set<BookAuthor>();
    public DbSet<BookGenre> BookGenres => Set<BookGenre>();

    public DBContext(DbContextOptions<DBContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Automatically generate Guid IDs
        modelBuilder.Entity<Book>()
            .Property(b => b.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Author>()
            .Property(a => a.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Genre>()
            .Property(g => g.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Review>()
            .Property(r => r.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<BookAuthor>()
            .HasKey(ba => new { ba.BookId, ba.AuthorId });

        modelBuilder.Entity<BookAuthor>()
            .HasOne(ba => ba.Book)
            .WithMany(b => b.BookAuthors)
            .HasForeignKey(ba => ba.BookId);

        modelBuilder.Entity<BookAuthor>()
            .HasOne(ba => ba.Author)
            .WithMany(a => a.BookAuthors)
            .HasForeignKey(ba => ba.AuthorId);

        modelBuilder.Entity<BookGenre>()
            .HasKey(bg => new { bg.BookId, bg.GenreId });

        modelBuilder.Entity<BookGenre>()
            .HasOne(bg => bg.Book)
            .WithMany(b => b.BookGenres)
            .HasForeignKey(bg => bg.BookId);

        modelBuilder.Entity<BookGenre>()
            .HasOne(bg => bg.Genre)
            .WithMany(g => g.BookGenres)
            .HasForeignKey(bg => bg.GenreId);

        modelBuilder.Entity<Review>()
            .HasOne(r => r.Book)
            .WithMany(b => b.Reviews)
            .HasForeignKey(r => r.BookId);
    }
}

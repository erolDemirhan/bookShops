using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAPI
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BooksAuthors>()
                .HasKey(x => new { x.AuthorId, x.BookId });

            modelBuilder.Entity<BooksGenres>()
                .HasKey(x => new { x.GenreId, x.BookId });

            modelBuilder.Entity<BookShopsBooks>()
                .HasKey(x => new { x.BookShopsId, x.BookId });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookShop> BookShops { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BooksAuthors> BookAuthors { get; set; }
        public DbSet<BooksGenres> BooksGenres { get; set; }
        public DbSet<BookShopsBooks> BookShopsBooks { get; set; }
        public DbSet<Rating> Ratings { get; set; }
    }
}

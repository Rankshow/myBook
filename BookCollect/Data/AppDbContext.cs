using BookCollect.Configuration;
using BookCollect.Models;
using Microsoft.EntityFrameworkCore;

namespace BookCollect.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookAuthorConfiguration());
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Book_Author> Book_Authors { get; set; }
        public DbSet<Log> Logs { get; set; }
    }
}

using BookCollect.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace BookCollect.Configuration
{
    public class BookAuthorConfiguration : IEntityTypeConfiguration<Book_Author>
    {
        public void Configure(EntityTypeBuilder<Book_Author> builder) 
        {
            //Configure many to many 
            builder
                .HasOne(b => b.Book)
                .WithMany(ba => ba.Book_Authors)
                .HasForeignKey(ba => ba.BookId);

            builder
                .HasOne(b => b.Author)
                .WithMany(bs => bs.Book_Authors)
                .HasForeignKey(r => r.AuthorId);

        }
    }
}

using BookCollect.Models;
using Microsoft.AspNetCore.Builder;

namespace BookCollect.Data
{
    public class AppDbInitalizer
    {
        public static void Seed(IApplicationBuilder applicatiobBuilder)
        {
            using (var serviceScope = applicatiobBuilder.ApplicationServices.CreateScope()) 
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                if (!context.Books.Any()) 
                {
                    context.Books.AddRange(new Book()
                    {
                        Title = "Jerro",
                        Description = "New Relased book",
                        IsRead = true,
                        DateRead = DateTime.Now.AddDays(-10),
                        Rate = 4,
                        Genre = "Biography",
                        CoverUrl = "https...",
                        DateAdded = DateTime.Now,
                    },
                    new Book()
                    {
                        Title = "War",
                        Description = "2nd Relased book",
                        IsRead = false,
                        Rate = 4,
                        Genre = "Biography",
                        CoverUrl = "https...",
                        DateAdded = DateTime.Now,
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}

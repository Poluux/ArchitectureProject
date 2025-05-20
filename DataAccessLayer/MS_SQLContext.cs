using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class MS_SQLContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public MS_SQLContext(DbContextOptions<MS_SQLContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=MS-SQL_Accounts")
                            .UseSeeding((context, _) =>
                            {
                                var user1 = new User() { Username = "John", Balance = 20, Quota = 15, IdCard = 555 };
                                var userJohn = context.Set<User>().FirstOrDefault(u => u.Username == user1.Username);
                                if (userJohn == null)
                                {
                                    context.Set<User>().Add(user1);
                                    context.SaveChanges();
                                }

                                var user2 = new User() { Username = "Anna", Balance = 10, Quota = 8, IdCard = 666 };
                                var userAnna = context.Set<User>().FirstOrDefault(u => u.Username == user2.Username);
                                if (userAnna == null)
                                {
                                    context.Set<User>().Add(user2);
                                    context.SaveChanges();
                                }
                            })
                            .UseAsyncSeeding(async (context, _, cancellationToken) =>
                            {
                                var user1 = new User() { Username = "John", Balance = 20, Quota = 15, IdCard = 555 };
                                var userJohn = await context.Set<User>().FirstOrDefaultAsync(u => u.Username == user1.Username);
                                if (userJohn == null)
                                {
                                    context.Set<User>().Add(user1);
                                    await context.SaveChangesAsync(cancellationToken);
                                }

                                var user2 = new User() { Username = "Anna", Balance = 10, Quota = 8, IdCard = 666 };
                                var userAnna = await context.Set<User>().FirstOrDefaultAsync(u => u.Username == user2.Username);
                                if (userAnna == null)
                                {
                                    context.Set<User>().Add(user2);
                                    await context.SaveChangesAsync();
                                }
                            });
            }

        }

    }
}

using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class PaymentContext : DbContext
    {
        public DbSet<Transaction> Transactions { get; set; }

        public PaymentContext(DbContextOptions<PaymentContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
               // optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=PaymentDB")
                optionsBuilder.UseSeeding((context, _) =>
                {
                    var transaction1 = new Transaction()
                    {
                        //UserId = 1,
                        Username = "John",
                        Amount = 10.0,
                        Date = DateTime.Now,
                        
                    };

                    var alreadyExists = context.Set<Transaction>().Any(t =>
                        //t.UserId == transaction1.UserId &&
                        t.Username == transaction1.Username &&
                        t.Amount == transaction1.Amount &&
                        t.Date.Date == transaction1.Date.Date);

                    if (!alreadyExists)
                    {
                        context.Set<Transaction>().Add(transaction1);
                        context.SaveChanges();
                    }
                })
                .UseAsyncSeeding(async (context, _, cancellationToken) =>
                {
                    var transaction1 = new Transaction()
                    {
                        //UserId = 1,
                        Username = "Alicia",
                        Amount = 10.0,
                        Date = DateTime.Now,
                        
                    };

                    var alreadyExists = await context.Set<Transaction>().AnyAsync(t =>
                       // t.UserId == transaction1.UserId &&
                       t.Username == transaction1.Username &&
                        t.Amount == transaction1.Amount &&
                        t.Date.Date == transaction1.Date.Date, cancellationToken);

                    if (!alreadyExists)
                    {
                        context.Set<Transaction>().Add(transaction1);
                        await context.SaveChangesAsync(cancellationToken);
                    }
                });

            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>()
                .HasKey(t => t.Id);

           /* modelBuilder.Entity<Transaction>()
                .HasOne(t => t.User)
                .WithMany(u => u.Transactions)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);*/
        }

    }
}

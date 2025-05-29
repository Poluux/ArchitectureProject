
using DataAccessLayer;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using WebAPI_ArchitectureProject.Business;

namespace WebAPI_ArchitectureProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IBalanceService, BalanceService>();
            builder.Services.AddScoped<IChargingService, ChargingService>();
            builder.Services.AddScoped<IQuotaConversionService, QuotaConversionService>();
            builder.Services.AddDbContext<MS_SQLContext>(options => options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=MS-SQL_Accounts"));
            builder.Services.AddDbContext<PaymentContext>(options => options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=PaymentDB"));


            var app = builder.Build();

            // Seeding de la base de données MS_SQLContext
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<MS_SQLContext>();
                SeedDatabase(context);
            }

            static void SeedDatabase(MS_SQLContext context)
            {
                var usersToAdd = new List<User>
                {
                    new User { Username = "John", Balance = 20, Quota = 15, IdCard = 555, Transactions = new List<Transaction>() },
                    new User { Username = "Anna", Balance = 10, Quota = 8, IdCard = 666, Transactions = new List<Transaction>() },
                    new User { Username = "Mike", Balance = 5, Quota = 10, IdCard = 101, Transactions = new List<Transaction>() },
                    new User { Username = "Sophie", Balance = 12, Quota = 9, IdCard = 102, Transactions = new List<Transaction>() },
                    new User { Username = "David", Balance = 7, Quota = 6, IdCard = 103, Transactions = new List<Transaction>() },
                    new User { Username = "Emma", Balance = 15, Quota = 20, IdCard = 104, Transactions = new List<Transaction>() },
                    new User { Username = "Liam", Balance = 8, Quota = 5, IdCard = 105, Transactions = new List<Transaction>() },
                    new User { Username = "Olivia", Balance = 14, Quota = 12, IdCard = 106, Transactions = new List<Transaction>() },
                    new User { Username = "Noah", Balance = 9, Quota = 7, IdCard = 107, Transactions = new List<Transaction>() },
                    new User { Username = "Ava", Balance = 6, Quota = 11, IdCard = 108, Transactions = new List<Transaction>() }
                };

                foreach (var user in usersToAdd)
                {
                    if (!context.Users.Any(u => u.Username == user.Username))
                    {
                        context.Users.Add(user);
                    }
                }

                context.SaveChanges();
            }


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

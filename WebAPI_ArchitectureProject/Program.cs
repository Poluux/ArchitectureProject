
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
                var user1 = new User { Username = "John", Balance = 20, Quota = 15, IdCard = 555 };
                var user2 = new User { Username = "Anna", Balance = 10, Quota = 8, IdCard = 666 };

                if (!context.Users.Any(u => u.Username == user1.Username))
                    context.Users.Add(user1);

                if (!context.Users.Any(u => u.Username == user2.Username))
                    context.Users.Add(user2);

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

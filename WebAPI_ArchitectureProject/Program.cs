
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;

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
            builder.Services.AddDbContext<MS_SQLContext>(options => options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=MS-SQL_Accounts"));
            builder.Services.AddDbContext<PaymentContext>(options => options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=PaymentDB"));


            var app = builder.Build();

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

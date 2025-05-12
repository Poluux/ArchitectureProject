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
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=MS-SQL_Accounts");

        }
    }
}

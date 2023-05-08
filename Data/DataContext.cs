using LoginRegistrationJWT.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace LoginRegistrationJWT.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
           : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=Generator\\SQLEXPRESS;Initial Catalog=LoginRegistrationJWT;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
    }
}

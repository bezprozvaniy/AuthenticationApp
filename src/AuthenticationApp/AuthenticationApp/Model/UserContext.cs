using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AuthenticationApp.Model
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Software> Software { get; set; }

        public UserContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.development.json");

            var config = builder.Build();
            optionsBuilder.UseSqlServer(config.GetConnectionString("AuthenticationAppDB"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User[]
                {
                    new User
                    {
                        Id = 1,
                        UserName = "serverName\\Ivan.Petrov",
                        LastName = "Ivan Petrov",
                        Email = "Ivan.Petrov@serverName"
                    },
                    new User
                    {
                        Id = 2,
                        UserName = "serverName\\Petr.Sidorov",
                        LastName = "Petr Sidorov",
                        Email = "Petr.Sidorov@serverName"
                    },
                    new User
                    {
                        Id = 3,
                        UserName = "serverName\\Sidor.Ivanov",
                        LastName = "Sidor Ivanov",
                        Email = "Sidor.Ivanov@serverName"
                    }
                });
            modelBuilder.Entity<Software>().HasData(
                new Software[]
                {
                    new Software { Id = 1, Name = "Windows 10", UserId = 1 },
                    new Software { Id = 2, Name = "IE 11.0", UserId = 1 },
                    new Software { Id = 3, Name = "Windows 7", UserId = 2 },
                    new Software { Id = 4, Name = "Firefox 67", UserId = 2 },
                    new Software { Id = 5, Name = "Windows XP", UserId = 3 },
                    new Software { Id = 6, Name = "Chrome 75", UserId = 3 }
                });
        }
    }
}

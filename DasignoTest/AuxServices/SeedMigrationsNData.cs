using DasignoTest.DBContext;
using DasignoTest.DTOs.userDTOs;
using DasignoTest.Entitys.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using static System.Net.Mime.MediaTypeNames;

namespace DasignoTest.AuxServices.Middleware
{
    public class SeedMigrationsNData 
    {
        private readonly AppDBContext dBContext;

        public SeedMigrationsNData(AppDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public async Task SeedDataAsync()
        {
           

                if (!dBContext.users.Any())
                {
                var users = new List<Users>
                {
                new Users { name = "John",     secondName = "A.", surName = "Doe",       secondSurName = "Smith",       birthDate = new DateTime(1990, 1, 15),  salary = 5000 },
                new Users { name = "Jane",     secondName = "B.", surName = "Doe",       secondSurName = "Johnson",     birthDate = new DateTime(1985, 5, 10),  salary = 6000 },
                new Users { name = "Michael",  secondName = "C.", surName = "Johnson",   secondSurName = "Brown",       birthDate = new DateTime(1992, 8, 22),  salary = 4500 },
                new Users { name = "Emily",    secondName = "D.", surName = "Smith",     secondSurName = "Taylor",      birthDate = new DateTime(1987, 12, 30), salary = 7000 },
                new Users { name = "David",    secondName = "E.", surName = "Brown",     secondSurName = "Williams",    birthDate = new DateTime(1995, 3, 17),  salary = 5500 },
                new Users { name = "Sophia",   secondName = "F.", surName = "Williams",  secondSurName = "Martinez",    birthDate = new DateTime(1991, 7, 8),   salary = 5200 },
                new Users { name = "Daniel",   secondName = "G.", surName = "Martinez",  secondSurName = "Davis",       birthDate = new DateTime(1993, 4, 28),  salary = 4900 },
                new Users { name = "Olivia",   secondName = "H.", surName = "Davis",     secondSurName = "Garcia",      birthDate = new DateTime(1988, 6, 14),  salary = 5800 },
                new Users { name = "James",    secondName = "I.", surName = "Garcia",    secondSurName = "Rodriguez",   birthDate = new DateTime(1994, 11, 5),  salary = 6200 },
                new Users { name = "Isabella", secondName = "J.", surName = "Rodriguez", secondSurName = "Wilson",      birthDate = new DateTime(1990, 9, 21),  salary = 5300 }
            };
                await dBContext.users.AddRangeAsync(users);
                await dBContext.SaveChangesAsync();
            }
            
        }   
    }
}

using ChamadosCRUD.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ChamadosCRUD.Models.Seeds
{
    public class SeedUserRoot
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ChamadosCRUDContext(
            serviceProvider.GetRequiredService<
            DbContextOptions<ChamadosCRUDContext>>()))
            {

                if (context.Users.Any())
                {
                    return;
                }
                var hasher = new PasswordHasher<User>();

                context.Users.AddRange(
                    new User
                    {
                        Id = 1,
                        Name = "Root",
                        RoleId = 1,
                        Email = "root@mail.com",
                        PasswordHash = hasher.HashPassword(null, "12345678"),
                    }
                );

                context.SaveChanges();
            }


        }
    }
}

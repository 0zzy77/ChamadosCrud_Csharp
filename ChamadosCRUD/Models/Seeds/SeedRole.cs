using Microsoft.EntityFrameworkCore;
using ChamadosCRUD.Data;

namespace ChamadosCRUD.Models.Seeds
{
    public class SeedRole
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ChamadosCRUDContext(
            serviceProvider.GetRequiredService<
            DbContextOptions<ChamadosCRUDContext>>()))
            {

                if (context.Role.Any())
                {
                    return;
                }

                context.Role.AddRange(
                    new Role
                    {   Id = 1,
                        Name = "Root"
                    },
                    new Role
                    {
                        Id = 2,
                        Name = "Administrador"
                    },
                    new Role
                    {
                        Id = 3,
                        Name = "Técnico"
                    }
                );

                context.SaveChanges();
            }
        }
    }
}

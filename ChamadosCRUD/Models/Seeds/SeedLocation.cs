using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ChamadosCRUD.Data;
using System;
using System.Linq;
using ChamadosCRUD.Models;

namespace ChamadosCRUD.Models.Seeds
{
    public class SeedLocation
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            //using (var context = new ChamadosCRUDContext(serviceProvider.GetRequiredService<DbContextOptions<ChamadosCRUDContext>>()))
            using (var context = new ChamadosCRUDContext(
            serviceProvider.GetRequiredService<
            DbContextOptions<ChamadosCRUDContext>>()))
            {
                
                if (context.Location.Any())
                {
                    return;
                }

                context.Location.AddRange(
                    new Location
                    {
                        Name = "Prefeitura Municipal de Santana de Parnaíba",
                        Description = "",
                        Latitude = -23.46502777485145,
                        Longitude = -46.93234662126453,
                        CEP = "06517520",
                        Rua = "Av. Mal. Mascarenhas de Moraes, 1283 - , Santana de Parnaíba - SP, 06517-520",
                        Bairro = "Sítio do Morro"
                    }
                );

                context.SaveChanges();
            }

            
        }
    }
}

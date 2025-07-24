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
                        CEP = "06517-520",
                        Rua = "Av. Mal. Mascarenhas de Moraes, 1283 - , Santana de Parnaíba - SP, 06517-520",
                        Bairro = "Sítio do Morro"
                    },
                    new Location
                    {
                        Name = "Hospital Municipal Santa Ana",
                        Description = "",
                        Latitude = -23.442134,
                        Longitude = -46.925057,
                        CEP = "06502-165",
                        Rua = "Rua Prof. Edgar de Moraes 707, Campo da Vila, SP",
                        Bairro = "Campo da Vila"
                    },
                    new Location
                    {
                        Name = "Secretaria de Educação",
                        Description = "",
                        Latitude = -23.445679, 
                        Longitude = -46.919909,
                        CEP = "06517-520",
                        Rua = "Rua Prof. Edgar de Morães 150 (Jardim Frediani)",
                        Bairro = "Jardim Frediani"
                    },
                    new Location
                    {
                        Name = "Usa Fazendinha",
                        Description = "",
                        Latitude = -23.405828, 
                        Longitude = -46.889305,
                        CEP = "06529-005",
                        Rua = "Estr. Ten. Marques 5441, Santana de Parnaíba, SP, 06529-005",
                        Bairro = "Fazendinha"
                    }
                );

                context.SaveChanges();
            }

            
        }
    }
}

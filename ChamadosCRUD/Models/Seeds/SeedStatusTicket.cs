using ChamadosCRUD.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ChamadosCRUD.Models.Seeds
{
    public class SeedStatusTicket
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            //using (var context = new ChamadosCRUDContext(serviceProvider.GetRequiredService<DbContextOptions<ChamadosCRUDContext>>()))
            using (var context = new ChamadosCRUDContext(
            serviceProvider.GetRequiredService<
            DbContextOptions<ChamadosCRUDContext>>()))
            {

                if (context.StatusTickets.Any())
                {
                    return;
                }

                context.StatusTickets.AddRange(
                    new StatusTicket
                    {   Id = 1,
                        Name = "Aberto"
                        
                    },
                    new StatusTicket
                    {
                        Id = 2,
                        Name = "Em análise"

                    },
                    new StatusTicket
                    {
                        Id = 3,
                        Name = "Aguardando atendimento"

                    },
                    new StatusTicket
                    {
                        Id = 4,
                        Name = "Em atendimento"

                    },
                    new StatusTicket
                    {
                        Id = 5,
                        Name = "Aguardando retorno do usuário"

                    },
                    new StatusTicket
                    {
                        Id = 6,
                        Name = "Aguardando terceiros"

                    },
                    new StatusTicket
                    {
                        Id = 7,
                        Name = "Fechado"

                    },
                    new StatusTicket
                    {
                        Id = 8,
                        Name = "Reaberto"

                    },
                    new StatusTicket
                    {
                        Id = 9,
                        Name = "Cancelado"

                    }
                );

                context.SaveChanges();
            }


        }
    }
}

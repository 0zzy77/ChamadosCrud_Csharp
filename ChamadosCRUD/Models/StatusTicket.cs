using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChamadosCRUD.Models
{
    public class StatusTicket
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

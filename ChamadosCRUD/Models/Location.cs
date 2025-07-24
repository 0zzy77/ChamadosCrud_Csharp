using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChamadosCRUD.Models
{
    public class Location
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Nome")]
        public string Name { get; set; }
        public string? Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        [Required]
        public string CEP { get; set; }
        [Required]
        public string Rua { get; set; }
        [Required]
        public string Bairro { get; set; }
    }
}

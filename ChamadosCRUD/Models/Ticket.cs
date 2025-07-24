using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChamadosCRUD.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        [Required, StringLength(60, MinimumLength = 10)]
        public string Title { get; set; }
        [Required, MinLength(80)]
        public string Description { get; set; }
        [Required, StringLength(60, MinimumLength = 5)]
        public string RequesterName { get; set; }
        [Required, EmailAddress]
        public string RequesterEmail { get; set; }
        [Phone]
        public string? RequesterPhone { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }
        [DataType(DataType.Date)]
        public DateTime UpdatedAt { get; set; }
        [Required]
        public int LocationId { get; set; }//Location
        [ForeignKey("LocationId")]
        public Location Location { get; set; }
        [Required]
        public int StatusTicketId { get; set; }//StatusTicket
        [ForeignKey("StatusTicketId")]
        public StatusTicket Status { get; set; }
        [Required]
        public int AssignedToId { get; set; }//User
        [ForeignKey("AssignedToId")]
        public User AssignedTo { get; set; }

        public Ticket()
        {
            CreatedAt = DateTime.Now.ToUniversalTime();
            UpdatedAt = DateTime.Now.ToUniversalTime();
        }
    }
}

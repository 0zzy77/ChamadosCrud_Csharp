using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChamadosCRUD.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        [Required, StringLength(60, MinimumLength = 10), Display(Name = "Título(assunto)")]
        public string Title { get; set; }
        [Required, MinLength(80), Display(Name = "Descrição do Problema")]
        public string Description { get; set; }
        [Required, StringLength(60, MinimumLength = 5), Display(Name = "Nome")]
        public string RequesterName { get; set; }
        [Required, EmailAddress, Display(Name = "E-mail")]
        public string RequesterEmail { get; set; }
        [Phone, Display(Name = "Contato")]
        public string? RequesterPhone { get; set; }
        [DataType(DataType.Date), Display(Name = "Criado em")]
        public DateTime CreatedAt { get; set; }
        [DataType(DataType.Date), Display(Name = "Atualizado em")]
        public DateTime UpdatedAt { get; set; }
        [Required, Display(Name = "Unidade")]
        public int LocationId { get; set; }//Location
        [ForeignKey("LocationId"), Display(Name = "Unidade")]
        public Location? Location { get; set; }
        public int StatusTicketId { get; set; }//StatusTicket
        [ForeignKey("StatusTicketId")]
        public StatusTicket? Status { get; set; }
        public int? AssignedToId { get; set; }//User
        [ForeignKey("AssignedToId"), Display(Name = "Atribuído para")]
        public User? AssignedTo { get; set; }

        public Ticket()
        {
            CreatedAt = DateTime.Now.ToUniversalTime();
            UpdatedAt = DateTime.Now.ToUniversalTime();
            StatusTicketId = 1;
        }
    }
}

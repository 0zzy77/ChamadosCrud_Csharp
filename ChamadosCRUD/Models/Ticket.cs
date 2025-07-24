using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChamadosCRUD.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório.")]
        [StringLength(60, MinimumLength = 10, ErrorMessage = "Mínimo de 10 e máximo de 60 caracteres.")]
        [Display(Name = "Título(assunto)")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório.")]
        [MinLength(80, ErrorMessage =" Mínimo de 80 caracteres.")]
        [Display(Name = "Descrição do Problema")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório.")]
        [StringLength(60, MinimumLength = 10, ErrorMessage = "Mínimo de 5 e máximo de 60 caracteres.")]
        [Display(Name = "Nome")]
        public string RequesterName { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato inválido.")]
        [Display(Name = "E-mail")]
        public string RequesterEmail { get; set; }
        [Phone(ErrorMessage = "Formato inválido. Exemplo: (99)999999999)")]
        [MinLength(13, ErrorMessage = "Formato inválido. Exemplo: (99)999999999)")]
        [Display(Name = "Contato")]
        public string? RequesterPhone { get; set; }
        [DataType(DataType.Date), Display(Name = "Criado em")]
        public DateTime CreatedAt { get; set; }
        [DataType(DataType.Date), Display(Name = "Atualizado em")]
        public DateTime UpdatedAt { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "Selecione uma unidade.")]
        [Display(Name = "Unidade")]
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

using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
namespace ChamadosCRUD.Models.ViewModels
{
    public class TicketViewModel
    {
    }

    public class TicketEditViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Selecione um Status.")]
        [Display(Name = "Status")]
        public int StatusId { get; set; }
        [Required(ErrorMessage = "Selecione um Técnico.")]
        [Display(Name = "Atribuído para")]
        public int? AssignedToId { get; set; }
        public IEnumerable <SelectListItem> Users { get; set; }
        public IEnumerable <SelectListItem> Status { get; set; }
    }
}

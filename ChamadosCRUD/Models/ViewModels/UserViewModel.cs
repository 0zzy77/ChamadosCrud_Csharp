using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChamadosCRUD.Models.ViewModels
{
    public class UserViewModel
    {
       
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!"), Display(Name = "Nome"), StringLength(60, MinimumLength = 5)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!"), EmailAddress, Display(Name = "E-mail"), StringLength(100)]
        public string Email { get; set; }

        //[BindNever]//pra nao ser incluido no formulario DÚVIDA--->(MESMO ASSIM NÃO FUNCIONAVA, APENAS ACRESCENTANDO ?, DESCOBRIR O PORQUE DEPOIS)
        [Display(Name = "Cargo")]
        public string? RoleName { get; set; }

        public IEnumerable<SelectListItem> Roles { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!")]
        [Display(Name = "Cargo")]
        public int RoleId { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!"), DataType(DataType.Password), Display(Name = "Senha")]
        public string Password { get; set; }
    }

    /*
     * sobre situação do RoleName dando erro de required mesmo sem [Required]
     * https://pt.stackoverflow.com/questions/287304/campo-sem-required-est%C3%A1-sendo-obrigat%C3%B3rio-por-qu%C3%AA
     */
}


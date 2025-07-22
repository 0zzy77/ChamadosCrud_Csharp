using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChamadosCRUD.Models
{

    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        
        public int Id { get; set; }
        [Required]
        [StringLength(60, MinimumLength = 5)]
        [Display(Name = "Nome")]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(100)]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Criado em")]
        public DateTime CreatedAt { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Atualizado em")]
        public DateTime? UpdatedAt { get; set; }
        [Required]
        [DefaultValue(3)]
        [Display(Name = "Cargo")]
        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public Role Role { get; set; }

        public User()
        {
            CreatedAt = DateTime.Now.ToUniversalTime();
            UpdatedAt = DateTime.Now.ToUniversalTime();
        }
    }
}

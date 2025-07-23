using System.ComponentModel.DataAnnotations;

namespace ChamadosCRUD.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<User> Users { get; set; }//caso precise fazer join inverso (pegar todos os usuários que utilizam determinado cargo)
    }
}

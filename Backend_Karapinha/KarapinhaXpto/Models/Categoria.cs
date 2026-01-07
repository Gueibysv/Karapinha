using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarapinhaAPI.Models
{
    
    public class Categoria
    {
      
        [Key]
        public int IDCategoria { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nome { get; set; }
        public ICollection<Profissional> Profissionais { get; set; }
        public ICollection<Servico> Servicos { get; set; }
    }
}

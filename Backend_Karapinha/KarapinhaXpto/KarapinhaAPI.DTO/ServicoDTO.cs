using KarapinhaAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarapinhaAPI.DTO
{
    public class ServicoDTO
    {

        [Key]
        public int IDServico { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }

        [ForeignKey("CategoriaDTO")]
        public int IDCategoria { get; set; }
        [Required]
        public decimal Preco { get; set; }

        public string Descricao { get; set; }
        
    }
}

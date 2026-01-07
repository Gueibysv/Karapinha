using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarapinhaAPI.Models
{
   
    public class Servico
    {
        
        [Key]
        public int IDServico { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }

        [ForeignKey("Categoria")]
        public int IDCategoria { get; set; }
        public Categoria Categoria { get; set; }

        [Required]
        public decimal Preco { get; set; }

        public string Descricao { get; set; }
        public ICollection<ServicoProfissional> ServicoProfissionais { get; set; }
        public ICollection<ServicoMarcacao> ServicoMarcacoes { get; set; }
    }
}

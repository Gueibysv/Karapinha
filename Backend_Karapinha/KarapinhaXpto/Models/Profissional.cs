using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarapinhaAPI.Models
{
    public class Profissional
    {
        [Key]
        public int IDProfissional { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }

        [ForeignKey("Categoria")]
        public int IDCategoria { get; set; }
        public Categoria Categoria { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(20)]
        public string BilheteIdentidade { get; set; }

        [MaxLength(15)]
        public string Telemovel { get; set; }

        [Required]
        [MaxLength(255)]
        public string Horario { get; set; }

        [Required]
        [MaxLength(255)]
        public string Senha { get; set; }  // Novo campo  de senha adicionado

        public ICollection<ServicoProfissional> ServicoProfissionais { get; set; }
        public ICollection<ServicoMarcacao> ServicoMarcacoes { get; set; }
    }
}

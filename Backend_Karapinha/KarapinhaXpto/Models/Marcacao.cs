using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarapinhaAPI.Models
{
    
    public class Marcacao
    {
        [Key]
        public int IDMarcacao { get; set; }
        [ForeignKey("Utilizador")]
        public int IDUtilizador { get; set; }
        public Utilizador Utilizador { get; set; }
        [Required]
        public DateTime Data { get; set; }
        [Required]
        public TimeSpan Hora { get; set; }
        [Required]
        [MaxLength(20)]
        public string Status { get; set; }
        public ICollection<ServicoMarcacao> ServicoMarcacoes { get; set; } = new HashSet<ServicoMarcacao>();
    }
}

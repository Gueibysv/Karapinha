using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarapinhaAPI.Models
{
    
    public class ServicoMarcacao
    {
       

        [ForeignKey("Marcacao")]
        public int IDMarcacao { get; set; }
        public Marcacao Marcacao { get; set; }

        [ForeignKey("Servico")]
        public int IDServico { get; set; }
        public Servico Servico { get; set; }

        [ForeignKey("Profissional")]
        public int IDProfissional { get; set; }
        public Profissional Profissional { get; set; }

        [Required]
        public DateTime Data { get; set; }

        [Required]
        public TimeSpan Hora { get; set; }
    }
}

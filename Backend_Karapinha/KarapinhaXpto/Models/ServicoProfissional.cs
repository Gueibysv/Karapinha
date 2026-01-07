using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarapinhaAPI.Models
{
    
    public class ServicoProfissional
    {
        
        [ForeignKey("Profissional")]
        public int IDProfissional { get; set; }
        public Profissional Profissional { get; set; }

        [ForeignKey("Servico")]
        public int IDServico { get; set; }
        public Servico Servico { get; set; }
    }
}

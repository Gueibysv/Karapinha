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
    public class ProfissionalDTO
    {
        [Key]
        public int IDProfissional { get; set; }

        public string Nome { get; set; }

        public int IDCategoria { get; set; }
      
        public string Email { get; set; }

        public string BilheteIdentidade { get; set; }
     
        public string Telemovel { get; set; }

        public string Horario { get; set; }
        public string Senha { get; set; }

    }
}

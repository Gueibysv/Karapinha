using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using KarapinhaAPI.Models;

namespace KarapinhaAPI.DTO
{
    public  class UtilizadorDTO
    {

        [Key]
        public int IDUtilizador { get; set; }

        public string NomeCompleto { get; set; }

        public string Email { get; set; }
       
        public string Telemovel { get; set; }
        public string BilheteIdentidade { get; set; }

        public string NomeUtilizador { get; set; }

        public string Senha { get; set; }

        public TipoUtilizador Tipo { get; set; }
        public StatusUtilizador Status { get; set; }
       
    }
    

}


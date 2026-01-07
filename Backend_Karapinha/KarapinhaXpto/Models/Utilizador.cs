using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KarapinhaAPI.Models
{
    
    public class Utilizador
    {
        [Key]
        public int IDUtilizador { get; set; }

        [Required]
        [MaxLength(100)]
        public string NomeCompleto { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(13)]
        public string Telemovel { get; set; }

        [Required]
        [MaxLength(14)]
        public string BilheteIdentidade { get; set; }

        [Required]
        [MaxLength(50)]
        public string NomeUtilizador { get; set; }

        [Required]
        [MaxLength(255)]
        public string Senha { get; set; }

        public TipoUtilizador Tipo { get; set; }
        public StatusUtilizador Status { get; set; }
        public ICollection<Marcacao> Marcacoes { get; set; }
    }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TipoUtilizador
    {
        NaoRegistado,
        Registado,
        Administrador,
        Administrativo
    }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum StatusUtilizador
    {
        Ativo,
        Inativo
    }

}


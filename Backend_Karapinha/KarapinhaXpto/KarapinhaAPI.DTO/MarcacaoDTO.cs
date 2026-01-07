using System;
using System.Collections.Generic;

namespace KarapinhaAPI.DTO
{
    public class MarcacaoDTO
    {
        public int IDMarcacao { get; set; }
        public int IDUtilizador { get; set; }
        public DateTime Data { get; set; }
        public string Hora { get; set; } // Mantido como string para a conversão de TimeSpan
        public string Status { get; set; }
        public ICollection<ServicoMarcacaoDTO> ServicoMarcacoes { get; set; } = new HashSet<ServicoMarcacaoDTO>();
    }
}

using System;

namespace KarapinhaAPI.DTO
{
    public class ServicoMarcacaoDTO
    {
        public int IDMarcacao { get; set; }
        public int IDServico { get; set; }
        public int IDProfissional { get; set; }
        public DateTime Data { get; set; }
        public string Hora { get; set; } // Mantido como string para a conversão de TimeSpan
    }
}

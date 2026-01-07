using KarapinhaAPI.Models;
using System.Collections.Generic;

namespace Shared.Interfaces
{
    public interface IMarcacaoInterface
    {
        ICollection<Marcacao> GetMarcacoes();
        Marcacao GetMarcacao(int Id);
        bool GetMarcacaoExiste(int MarcacaoID);
        ICollection<Marcacao> GetsMarcacoesByServicos(int Id);

        // Novo método para consultar marcações por ID do utilizador
        ICollection<Marcacao> GetMarcacoesByUtilizador(int id);
        ICollection<Marcacao> GetMarcacoesByProfissional(int idProfissional);

        bool CreateMarcacao(Marcacao marcacao);
        bool DeleteMarcacao(Marcacao marcacao);
        bool UpdateMarcacao(Marcacao marcacao);

        bool save();
    }
}

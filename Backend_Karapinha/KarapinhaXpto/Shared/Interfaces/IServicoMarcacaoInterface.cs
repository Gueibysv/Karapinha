using KarapinhaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Interfaces
{
    public interface IServicoMarcacaoInterface
    {
        ICollection<ServicoMarcacao> GetServicosMarcacoes();
        ServicoMarcacao GetMarcacao(int Id);

        bool GetMarcacaoExiste(int MarcacaoID);
        ICollection<Marcacao> GetsMarcacoesByServicos(int Id);
    }
}

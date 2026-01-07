using KarapinhaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Interfaces
{
    public interface IServicoInterface
    {
        ICollection<Servico> GetServicos();
        Servico GetServico(int Id);
   
        bool GetServicoExiste(int ServicoID);
        ICollection<Servico> GetServicoByProfissionais(int Id);
        ICollection<Servico> GetsServicoByMarcacoes(int Id);

        bool CreateServico(Servico servico);
        bool DeleteServico(Servico servico);
        bool UpdateServico(Servico servico);
        bool save();

    }
}

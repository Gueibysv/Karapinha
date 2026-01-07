using KarapinhaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Interfaces
{
    public interface IUtilizadorInterface
    {

        ICollection<Utilizador> GetUtilizadores();
        Utilizador GetUtilizador(int id);
        Utilizador GetUtilizador (string name);
        bool UtilizadorExiste(int UtilizadorID);
        bool CreateUtilizador(Utilizador utilizador);
        bool UpdateUtilizador(Utilizador utilizador);
        bool AtivarUtilizador(Utilizador utilizador);
        bool save();
    }
}

using KarapinhaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Interfaces
{
    public interface IProfissionalInterface
    {
        ICollection<Profissional> GetProfissionais();
        Profissional GetProfissional(int id);
        Profissional Getprofissional(string nome);
        bool GetProssionalExiste(int ProfissionalID);
        ICollection<Profissional> GetServicosByProfissional(int Id);
        ICollection<Profissional> GetsMarcacoesByProfissional(int Id);
        bool CreateProfissional(Profissional profissional);
        bool DeleteProfissional(Profissional profissional);
        bool UpdateProfissional(Profissional profissional);
        bool save();



    }
}

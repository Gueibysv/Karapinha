using KarapinhaAPI.DAL;
using KarapinhaAPI.Models;
using Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Repository
{
    public class ProfissionalRepository: IProfissionalInterface
    {

        private DataContext _context;
        public ProfissionalRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateProfissional(Profissional profissional)
        {
            _context.Add(profissional);
            return save();
        }

        public bool DeleteProfissional(Profissional profissional)
        {
            _context.Remove(profissional);
            return save();
        }

        public ICollection<Profissional> GetProfissionais()
        {
            return _context.Profissionais.ToList(); 
        }

        public Profissional GetProfissional(int id)
        {
            return _context.Profissionais.Where(p => p.IDProfissional == id).FirstOrDefault();
        }

        public Profissional Getprofissional(string nome)
        {
            return _context.Profissionais.Where(p => p.Nome == nome).FirstOrDefault();
        }

        public bool GetProssionalExiste(int ProfissionalID)
        {
            return _context.Profissionais.Any(p => p.IDProfissional == ProfissionalID);
        }

        public ICollection<Profissional> GetServicosByProfissional(int Id)
        {
            return _context.ServicoProfissionais.Where(p => p.IDServico == Id).Select(c => c.Profissional).ToList();
        }

        public ICollection<Profissional> GetsMarcacoesByProfissional(int Id)
        {
            return _context.ServicoMarcacoes.Where(p => p.IDMarcacao == Id).Select(c => c.Profissional).ToList();
        }

        public bool save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateProfissional(Profissional profissional)
        {
            _context.Update(profissional);
            return save();
        }
    }
}

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
    public class ServicoRepository : IServicoInterface
    {
        private DataContext _context;
        public ServicoRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateServico(Servico servico)
        {
            _context.Add(servico);
            return save();
        }

        public bool DeleteServico(Servico servico)
        {
            _context.Remove(servico);
            return save();
        }

        public Servico GetServico(int Id)
        {
            return _context.Servicos.Where(s => s.IDServico == Id).FirstOrDefault();
        }

        public ICollection<Servico> GetServicoByProfissionais(int Id)
        {
            return _context.ServicoProfissionais.Where(sp => sp.IDProfissional == Id).Select(Sp => Sp.Servico).ToList();
        }

        public bool GetServicoExiste(int ServicoID)
        {
            return _context.Servicos.Any(s => s.IDServico == ServicoID);
        }
        public ICollection<Servico> GetServicos()
        {
            return _context.Servicos.ToList();
        }

        public ICollection<Servico> GetsServicoByMarcacoes(int Id)
        {
            return _context.ServicoMarcacoes.Where(sp => sp.IDMarcacao == Id).Select(Sp => Sp.Servico).ToList();
        }

        public bool save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateServico(Servico servico)
        {
            _context.Update(servico);
            return save();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KarapinhaAPI.DAL;
using KarapinhaAPI.Models;
using Shared.Interfaces;

namespace Shared.Repository
{
    public class UtilizadorRepository : IUtilizadorInterface
    {
        private readonly DataContext _context;
        public UtilizadorRepository (DataContext context) { 
        
            _context = context;
        }

        public bool AtivarUtilizador(Utilizador utilizador)
        {
            _context.Update(utilizador);
            return save();
        }

        public bool CreateUtilizador(Utilizador utilizador)
        {
            _context.Add(utilizador);
            return save();
        }

        public Utilizador GetUtilizador(int id)
        {
            return _context.Utilizadores.Where(p => p.IDUtilizador == id).FirstOrDefault();
        }

        public Utilizador GetUtilizador(string name)
        {
            return _context.Utilizadores.Where(p => p.NomeCompleto == name).FirstOrDefault ();
        }

        public ICollection<Utilizador> GetUtilizadores() { 
        
            return _context.Utilizadores.OrderBy(u => u.IDUtilizador).ToList();
            }

        public bool save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateUtilizador(Utilizador utilizador)
        {
            _context.Update(utilizador);
            return save();
        }

        public bool UtilizadorExiste(int UtilizadorID)
        {
            return _context.Utilizadores.Any(p => p.IDUtilizador == UtilizadorID);
        }
    }
}

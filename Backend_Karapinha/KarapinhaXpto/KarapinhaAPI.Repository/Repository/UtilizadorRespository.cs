using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KarapinhaAPI.DAL;
using KarapinhaAPI.Models;

namespace Shared.Repository
{
   
    public class UtilizadorRespository
    {
        private readonly DataContext _context;

        public UtilizadorRespository (DataContext context)
        {
            _context = context;
        }
        public ICollection <Utilizador> GetUtilizadores()
        {
            return _context.Utilizadores.OrderBy(u =>u.IDUtilizador).ToList();
        }

    }
}

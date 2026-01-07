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
    public class CategoriaRepository : ICategoriaInterface
    {
        private DataContext _context;
        public CategoriaRepository(DataContext context)
        {
            _context = context;
        }
        public Categoria GetCategoria(string nome)
        {
            return _context.Categorias.Where(c => c.Nome == nome).FirstOrDefault();
        }

        public bool GetCategoriaExiste(int categoriaID)
        {
            return _context.Categorias.Any(c => c.IDCategoria == categoriaID);
        }

        public ICollection<Categoria> GetCategorias()
        {
            return _context.Categorias.ToList();
        }

        public Categoria GetCategoria(int id)
        {
            return _context.Categorias.Where(c => c.IDCategoria == id).FirstOrDefault();
        }

        public ICollection<Profissional> GetProfissionaisByCategoria(int Id)
        {
            return _context.Categorias.Where(p => p.IDCategoria == Id).SelectMany(p => p.Profissionais).ToList();
        }

        public ICollection<Categoria> GetsServicoByCategoria(int Id)
        {
            return _context.Servicos.Where(s => s.IDServico == Id).Select(c => c.Categoria).ToList();
        }

        public bool CreateCategoria(Categoria categoria)
        {
            _context.Add(categoria);         
            return save();
        }

        public bool save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool DeleteCategoria(Categoria categoria)
        {
            _context.Remove(categoria);
            return save();
        }

        public bool UpdateCategoria(Categoria categoria)
        {
          _context.Update(categoria);
            return save();
        }
    }
}

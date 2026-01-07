using KarapinhaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Interfaces
{
    public interface ICategoriaInterface
    {
        ICollection<Categoria> GetCategorias();
        Categoria GetCategoria(int id);
        Categoria GetCategoria(string nome);
        bool GetCategoriaExiste(int categoriaID);
        ICollection<Profissional> GetProfissionaisByCategoria(int Id);
        ICollection<Categoria> GetsServicoByCategoria(int Id);

        bool CreateCategoria (Categoria categoria);
        bool DeleteCategoria(Categoria categoria);
        bool UpdateCategoria(Categoria categoria);
        bool save();
       



    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using KarapinhaAPI.DAL;
using KarapinhaAPI.Models;
using Microsoft.EntityFrameworkCore;
using Shared.Interfaces;

namespace Shared.Repository
{
    public class MarcacaoRepository : IMarcacaoInterface
    {
        private DataContext _context;
        public MarcacaoRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateMarcacao(Marcacao marcacao)
        {
            _context.Marcacoes.Add(marcacao);
            return save();
        }

        public bool DeleteMarcacao(Marcacao marcacao)
        {
            _context.Marcacoes.Remove(marcacao);
            return save();
        }

        public Marcacao GetMarcacao(int Id)
        {
            return _context.Marcacoes
                .Include(m => m.ServicoMarcacoes)
                    .ThenInclude(sm => sm.Servico)
                .Include(m => m.ServicoMarcacoes)
                    .ThenInclude(sm => sm.Profissional)
                .FirstOrDefault(m => m.IDMarcacao == Id);
        }

        public bool GetMarcacaoExiste(int MarcacaoID)
        {
            return _context.Marcacoes.Any(m => m.IDMarcacao == MarcacaoID);
        }

        public ICollection<Marcacao> GetMarcacoes()
        {
            return _context.Marcacoes
                .Include(m => m.ServicoMarcacoes)
                    .ThenInclude(sm => sm.Servico)
                .Include(m => m.ServicoMarcacoes)
                    .ThenInclude(sm => sm.Profissional)
                .ToList();
        }

        public ICollection<Marcacao> GetMarcacoesByUtilizador(int Id)
        {
            return _context.Marcacoes
                .Where(m => m.IDUtilizador == Id)
                .Include(m => m.ServicoMarcacoes)
                    .ThenInclude(sm => sm.Servico)
                .Include(m => m.ServicoMarcacoes)
                    .ThenInclude(sm => sm.Profissional)
                .ToList();
        }

        public ICollection<Marcacao> GetMarcacoesByProfissional(int idProfissional)
        {
            return _context.Marcacoes
                .Where(m => m.ServicoMarcacoes.Any(sm => sm.IDProfissional == idProfissional))
                .Include(m => m.ServicoMarcacoes)
                    .ThenInclude(sm => sm.Servico)
                .Include(m => m.ServicoMarcacoes)
                    .ThenInclude(sm => sm.Profissional)
                .ToList();
        }

        public ICollection<Marcacao> GetsMarcacoesByServicos(int Id)
        {
            return _context.ServicoMarcacoes
                .Where(sm => sm.IDServico == Id)
                .Select(sm => sm.Marcacao)
                .ToList();
        }

        public bool save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool UpdateMarcacao(Marcacao marcacao)
        {
            _context.Marcacoes.Update(marcacao);
            return save();
        }
    }
}

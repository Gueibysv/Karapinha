using KarapinhaAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarapinhaAPI.DAL
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions <DataContext> options ): base (options) { 
        
            
        
        }
        public DbSet<Utilizador> Utilizadores { get; set; }
        public DbSet<Profissional> Profissionais { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Servico> Servicos { get; set; }
        public DbSet<Marcacao> Marcacoes { get; set; }
        public DbSet<ServicoMarcacao> ServicoMarcacoes { get; set; }
        public DbSet<ServicoProfissional> ServicoProfissionais { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ServicoProfissional>()
                .HasKey(sp => new { sp.IDProfissional, sp.IDServico }); // Chave primária composta da tabela associativa
            modelBuilder.Entity<ServicoProfissional>()
                .HasOne(p => p.Profissional)
                .WithMany(sp => sp.ServicoProfissionais)
                .HasForeignKey(p => p.IDProfissional)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ServicoProfissional>()
                .HasOne(s => s.Servico)
                .WithMany(sp => sp.ServicoProfissionais)
                .HasForeignKey(s => s.IDServico)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ServicoMarcacao>()
                 .HasKey(sm => new { sm.IDMarcacao, sm.IDServico, sm.IDProfissional }); // chave primária composta da tabela associativa
            modelBuilder.Entity<ServicoMarcacao>()
                .HasOne(m => m.Marcacao)
                .WithMany(sm => sm.ServicoMarcacoes)
                .HasForeignKey(m => m.IDMarcacao)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ServicoMarcacao>()
                .HasOne(s => s.Servico)
                .WithMany(sm => sm.ServicoMarcacoes)
                .HasForeignKey(s => s.IDServico)    
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ServicoMarcacao>()
                .HasOne(p => p.Profissional)
                .WithMany(sm => sm.ServicoMarcacoes)
                .HasForeignKey(p => p.IDProfissional);

            modelBuilder.Entity<Marcacao>()
            .HasOne(m => m.Utilizador)        // Uma marcação tem um utilizador associado
            .WithMany(u => u.Marcacoes)      // Um usuário pode ter várias marcações
            .HasForeignKey(u => u.IDUtilizador);


            modelBuilder.Entity<Profissional>()
            .HasOne(p => p.Categoria)           // Um profissional tem uma categoria associado
            .WithMany(c => c.Profissionais)     // Uma categoria poder ter vários profissionais
            .HasForeignKey(p => p.IDCategoria);

            modelBuilder.Entity<Servico>()
           .HasOne(s => s.Categoria)
           .WithMany(c => c.Servicos)
           .HasForeignKey(s => s.IDCategoria);

            modelBuilder.Entity<Servico>()
                .Property(s => s.Preco)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Utilizador>()
               .Property(u => u.Tipo)
               .HasConversion(
                   v => v.ToString(),
                   v => (TipoUtilizador)Enum.Parse(typeof(TipoUtilizador), v));

            modelBuilder.Entity<Utilizador>()
                .Property(u => u.Status)
                .HasConversion(
                    v => v.ToString(),
                    v => (StatusUtilizador)Enum.Parse(typeof(StatusUtilizador), v));







        }

    }
}


using System;
using System.Linq;
using KarapinhaAPI.DAL;
using KarapinhaAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public class SeedHelpers
{

    private readonly DataContext context;
    public SeedHelpers(DataContext context)
    {
        this.context = context;
    }
    public void SeedDataContext()
    {
        context.Database.EnsureCreated();

        // Seed Users
        if (!context.Utilizadores.Any())
        {
            var Utilizadores = new Utilizador[]
            {
                new Utilizador { NomeCompleto = "Alice Silva", Email = "alice@exemplo.com", Telemovel = "912345678", BilheteIdentidade = "12345678", NomeUtilizador = "alice", Senha = "password", Tipo = TipoUtilizador.Administrativo, Status = StatusUtilizador.Ativo },
                new Utilizador { NomeCompleto = "Bob Pereira", Email = "bob@exemplo.com", Telemovel = "987654321", BilheteIdentidade = "87654321", NomeUtilizador = "bob", Senha = "password", Tipo = TipoUtilizador.Registado, Status =  StatusUtilizador.Ativo }
            };
            foreach (var Utilizador in Utilizadores)
            {
                context.Utilizadores.Add(Utilizador);
            }
            context.SaveChanges();
        }

        // Seed Categories
        if (!context.Categorias.Any())
        {
            var categorias = new Categoria[]
            {
                new Categoria { Nome = "Cabelo" },
                new Categoria { Nome = "Estética" }
            };
            foreach (var categoria in categorias)
            {
                context.Categorias.Add(categoria);
            }
            context.SaveChanges();
        }

        // Seed Services
        if (!context.Servicos.Any())
        {
            var servicos = new Servico[]
            {
                new Servico { Nome = "Corte de Cabelo", IDCategoria = context.Categorias.Single(c => c.Nome == "Cabelo").IDCategoria, Preco = 15.00m, Descricao = "Corte de cabelo masculino e feminino" },
                new Servico { Nome = "Manicure", IDCategoria = context.Categorias.Single(c => c.Nome == "Estética").IDCategoria, Preco = 20.00m, Descricao = "Manicure completa" }
            };
            foreach (var servico in servicos)
            {
                context.Servicos.Add(servico);
            }
            context.SaveChanges();
        }

        // Seed Professionals
        if (!context.Profissionais.Any())
        {
            var profissionais = new Profissional[]
            {
                new Profissional { Nome = "Carlos Mendes", IDCategoria = context.Categorias.Single(c => c.Nome == "Cabelo").IDCategoria, Email = "carlos@exemplo.com", BilheteIdentidade = "11223344", Telemovel = "934567890", Horario = "09:00, 10:30, 14:00" },
                new Profissional { Nome = "Diana Lima", IDCategoria = context.Categorias.Single(c => c.Nome == "Estética").IDCategoria, Email = "diana@exemplo.com", BilheteIdentidade = "44332211", Telemovel = "945678901", Horario = "10:00, 12:30, 15:00" }
            };
            foreach (var profissional in profissionais)
            {
                context.Profissionais.Add(profissional);
            }
            context.SaveChanges();
        }
    }
}
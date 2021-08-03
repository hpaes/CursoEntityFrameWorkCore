using System;
using System.Linq;
using CursoEFCore.Domain;
using CursoEFCore.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace CursoEFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            // using var db = new Data.ApplicationContext();

            // Indicado para utilizar somente em produção
            // db.Database.Migrate();

            // var migrationExists = db.Database.GetPendingMigrations().Any();
            // if (migrationExists)
            // {
            //     Console.WriteLine("Pending migration exists");
            // }

            // InserirDados();
            // InserirDadosEmMassa();
            ConsultarDados();
        }

        private static void ConsultarDados()
        {
            using var db = new Data.ApplicationContext();
            // var consultaPorSintaxe = (from c in db.Clientes where c.Id > 0 select c).ToList();
            var consultaPorMetodo = db.Clientes
                .Where(p => p.Id > 0)
                .OrderBy(p => p.Id)
                .ToList();

            foreach (var cliente in consultaPorMetodo)
            {
                Console.WriteLine($"Consultando Cliente: {cliente.Id}");
                // db.Clientes.Find(cliente.Id);
                db.Clientes.FirstOrDefault(p => p.Id == cliente.Id);
            }
        }

        private static void InserirDadosEmMassa()
        {
            var produto = new Produto
            {
                Descricao = "Produto Teste",
                CodigoBarras = "1234567891231",
                Valor = 10m,
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = true,
            };

            var cliente = new Cliente
            {
                Nome = "Herbert Paes",
                CEP = "50290120",
                Cidade = "Recife",
                Estado = "PE",
                Telefone = "8191919090"
            };

            var listaClientes = new[]
            {
                new Cliente
                {
                    Nome = "Teste 1",
                    CEP = "50290120",
                    Cidade = "Recife",
                    Estado = "PE",
                    Telefone = "8191919090"
                },
                new Cliente
                {
                    Nome = "Teste 2",
                    CEP = "50290120",
                    Cidade = "Recife",
                    Estado = "PE",
                    Telefone = "8191919090"
                }
            };

            using var db = new Data.ApplicationContext();
            // db.AddRange(produto, cliente);

            db.Set<Cliente>().AddRange(listaClientes);
            // db.Clientes.AddRange(listaClientes);

            var registros = db.SaveChanges();
            Console.WriteLine($"Total registros: { registros }");
        }
        private static void InserirDados()
        {
            var produto = new Produto
            {
                Descricao = "Produto Teste",
                CodigoBarras = "1234567891231",
                Valor = 10m,
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = true,
            };

            using var db = new Data.ApplicationContext();
            // Primeira opção
            db.Produtos.Add(produto);
            // Segunda opção
            // db.Set<Produto>().Add(produto);
            // Terceira opção
            // db.Entry(produto).State = EntityState.Added;
            // Quarta opção
            // db.Add(produto);

            var registros = db.SaveChanges();
            Console.WriteLine($"Total Registros(s): {registros}");

        }
    }
}

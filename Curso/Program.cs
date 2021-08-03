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

            InserirDados();
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

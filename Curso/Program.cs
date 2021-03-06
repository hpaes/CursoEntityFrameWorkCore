using System;
using System.Collections.Generic;
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
            // ConsultarDados();
            // CadastrarPedido();
            // ConsultarPedidoCarregamentoAdiantado();
            // AtualizarDados();
            RemoverRegistro();
        }
        // cpf 088.223.414-57

        private static void RemoverRegistro()
        {
            using var db = new Data.ApplicationContext();
            // var cliente = db.Clientes.Find(2);
            var cliente = new Cliente { Id = 2 };

            // db.Clientes.Remove(cliente);
            // db.Remove(cliente);
            db.Entry(cliente).State = EntityState.Deleted;

            db.SaveChanges();
        }
        private static void AtualizarDados()
        {
            using var db = new Data.ApplicationContext();
            // var cliente = db.Clientes.Find(1);

            var cliente = new Cliente
            {
                Id = 1
            };

            var clienteDesconectado = new
            {
                Nome = "Cliente Desconectado passo 3",
                Telefone = "1312341414",
            };

            db.Attach(cliente);
            db.Entry(cliente).CurrentValues.SetValues(clienteDesconectado);

            // db.Entry(cliente).State = EntityState.Modified;
            // db.Clientes.Update(cliente);
            db.SaveChanges();
        }
        private static void ConsultarPedidoCarregamentoAdiantado()
        {
            using var db = new Data.ApplicationContext();
            var pedidos = db.Pedidos
                .Include(p => p.Itens)
                    .ThenInclude(p => p.Produto)
                .ToList();

            Console.WriteLine(pedidos.Count);
        }
        private static void CadastrarPedido()
        {
            using var db = new Data.ApplicationContext();

            var cliente = db.Clientes.FirstOrDefault();
            var produto = db.Produtos.FirstOrDefault();

            var pedido = new Pedido
            {
                ClienteId = cliente.Id,
                IniciadoEm = DateTime.Now,
                FinalizadoEm = DateTime.Now,
                Observacao = "Pedido Teste",
                Status = StatusPedido.Analise,
                TipoFrete = TipoFrete.SemFrete,
                Itens = new List<PedidoItem> {

                    new PedidoItem
                    {
                        ProdutoId = produto.Id,
                        Desconto = 0,
                        Quantidade = 1,
                        Valor = 10
                    }
                }
            };

            db.Pedidos.Add(pedido);

            db.SaveChanges();
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

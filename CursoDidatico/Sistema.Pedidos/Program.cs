using Microsoft.EntityFrameworkCore;
using Sistema.Pedidos.Domain;
using Sistema.Pedidos.ValueObjetcs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sistema.Pedidos
{
    class Program
    {
        static void Main(string[] args)
        {
            //using var db = new Data.ApplicationContext();

            //db.Database.Migrate();

            /*var existe = db.Database.GetPendingMigrations().Any();
            if(existe)
            {
                // 
            }*/

            //InserirDados();
            //InserirDadosEmMassa();
            //ConsultarDados();
            //CadastrarPedido();
            //ConsultarPedidoCarregamentoAdiantado();
            AtualizarDados();
        }

        private static void AtualizarDados() 
        {
            using var db = new Data.ApplicationContext();
            //var cliente = db.Clientes.Find(1);
            //cliente.Nome = "Cliente Alterado Passo 2";

            var cliente = new Cliente
            {
                Id = 1
            };

            var clienteDesconectado = new
            {
                Nome = "Cliente Desconectado Teste 1",
                Telefone = "999222054"
            };
            db.Attach(cliente); 
            db.Entry(cliente).CurrentValues.SetValues(clienteDesconectado);
            
            //db.Entry(cliente).State = EntityState.Modified;
            //db.Clientes.Update(cliente);
            db.SaveChanges();
        }
        private static void ConsultarPedidoCarregamentoAdiantado()
        {
            using var db = new Data.ApplicationContext();
            var pedidos = db
                .Pedidos
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
                TipoFrete = TipoFrete.SemFrete,
                Status = StatusPedido.Analise,
                Observacao = "Pedido Teste",
                Itens = new List<PedidoItem>
                 {
                     new PedidoItem
                     {
                         ProdutoId = produto.Id,
                         Desconto = 0,
                         Quantidade = 1,
                         Valor = 10,
                     }
                 }
            };
            db.Pedidos.Add(pedido);
            db.SaveChanges();
        }

        private static void ConsultarDados()
        {
            using var db = new Data.ApplicationContext();
            //var consultaProSintaxe = (from c in db.Clientes where c.Id > 0 select c).ToList();
            var consultaPorMetodo = db.Clientes
                .Where(p => p.Id > 0)
                .OrderBy(p => p.Id)
                .ToList();
            foreach (var cliente in consultaPorMetodo)
            {
                Console.WriteLine($"Consultando cliente: {cliente.Id}");
                //db.Clientes.Find(cliente.Id);
                db.Clientes.FirstOrDefault(p => p.Id == cliente.Id);
            }
        }
        private static void InserirDadosEmMassa()
        {
            var produto = new Produto
            {
                Descricao = "Produto Teste 1",
                CodigoBarras = "1234567891234",
                Valor = 10m,
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };

            var cliente = new Cliente
            {
                Nome = "Cliente Teste 1",
                CEP = "99996000",
                Cidade = "Juara",
                Estado = "MT",
                Telefone = "999222054"
            };

            var listaCliente = new[]
            {

                new Cliente
                {
                Nome = "Cliente teste 2",
                CEP = "99599000",
                Cidade = "Juara",
                Estado = "MT",
                Telefone = "999252054"
                },

                new Cliente
                {
                Nome = "Cliente teste 3",
                CEP = "99999000",
                Cidade = "Juara",
                Estado = "MT",
                Telefone = "999622054"
                }
        };

            using var db = new Data.ApplicationContext();
            //db.AddRange(produto, cliente);
            //db.Clientes.AddRange(listaCliente);
            db.AddRange(listaCliente);

            var registros = db.SaveChanges();

            Console.WriteLine($"Total Registro(s): { registros }");
        }
        private static void InserirDados()
        {
            var produto = new Produto
            {
                Descricao = "Produto 1",
                CodigoBarras = "1234567891234",
                Valor = 10m,
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };

            using var db = new Data.ApplicationContext();
            //db.Set<Produto>().Add(produto);
            //db.Produtos.Add(produto);
            //db.Entry(produto).State = EntityState.Added;
            db.Add(produto);

            var registros = db.SaveChanges();

            Console.WriteLine($"Total Registro(s): { registros }");
        }
    }
}

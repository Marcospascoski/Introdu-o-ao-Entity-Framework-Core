using Microsoft.EntityFrameworkCore;
using Sistema.Pedidos.Domain;
using Sistema.Pedidos.ValueObjetcs;
using System;
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
            InserirDadosEmMassa();
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

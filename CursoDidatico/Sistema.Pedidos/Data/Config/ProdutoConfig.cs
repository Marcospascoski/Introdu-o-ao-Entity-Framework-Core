using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Pedidos.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Pedidos.Data.Config
{
    public class ProdutoConfig : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produtos");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.CodigoBarras)
            .HasColumnType("VARCHAR(14)")
            .IsRequired();
            builder.Property(p => p.Descricao)
            .HasColumnType("VARCHAR(60)");
            builder.Property(p => p.Valor).IsRequired();
            builder.Property(p => p.TipoProduto)
            .HasConversion<string>();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using CarrinhoDeCompras.Entities;

namespace CarrinhoDeCompras.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Carrinho> Carrinhos { get; set; }
        public DbSet<ItemCarrinho> ItensCarrinhos { get; set; }
    }
}

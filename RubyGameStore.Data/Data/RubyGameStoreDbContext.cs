using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RubyGameStore.Models.Models;

namespace RubyGameStore.Data.Data
{
    public class RubyGameStoreDbContext : IdentityDbContext
    {
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Plataforma> Plataformas { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Carrinho> Carrinhos { get; set; }

        public RubyGameStoreDbContext(DbContextOptions<RubyGameStoreDbContext> options) : base(options)
        {

        }
    }
}

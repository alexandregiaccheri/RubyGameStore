using Microsoft.EntityFrameworkCore;
using RubyGameStore.Models.Models;

namespace RubyGameStore.Data.Data
{
    public class RubyGameStoreDbContext : DbContext
    {
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Plataforma> Plataformas { get; set; }

        public RubyGameStoreDbContext(DbContextOptions<RubyGameStoreDbContext> options) : base(options)
        {

        }
    }
}

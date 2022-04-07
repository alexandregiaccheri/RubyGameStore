using RubyGameStore.Data.Data;
using RubyGameStore.Data.Repository.IRepository;
using RubyGameStore.Models.Models;

namespace RubyGameStore.Data.Repository
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        private readonly RubyGameStoreDbContext dbContext;
        public UsuarioRepository(RubyGameStoreDbContext context) : base(context)
        {
            dbContext = context;
        }
    }
}

using RubyGameStore.Data.Data;
using RubyGameStore.Data.Repository.IRepository;
using RubyGameStore.Models.Models;

namespace RubyGameStore.Data.Repository
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        private readonly RubyGameStoreDbContext dbContext;

        public CategoriaRepository(RubyGameStoreDbContext context) : base(context)
        {
            dbContext = context;
        }

        public void Update(Categoria categoria)
        {
            dbContext.Categorias.Update(categoria);
        }
    }
}

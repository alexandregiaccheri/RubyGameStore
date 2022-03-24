using RubyGameStore.Data.Data;
using RubyGameStore.Data.Repository.IRepository;
using RubyGameStore.Models.Models;

namespace RubyGameStore.Data.Repository
{
    public class PlataformaRepository : Repository<Plataforma>, IPlataformaRepository
    {
        private readonly RubyGameStoreDbContext dbContext;

        public PlataformaRepository(RubyGameStoreDbContext context) : base(context)
        {
            dbContext = context;
        }

        public void Update(Plataforma obj)
        {
            dbContext.Plataformas.Update(obj);
        }
    }
}

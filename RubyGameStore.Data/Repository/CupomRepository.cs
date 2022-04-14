using RubyGameStore.Data.Data;
using RubyGameStore.Data.Repository.IRepository;
using RubyGameStore.Models.Models;

namespace RubyGameStore.Data.Repository
{
    public class CupomRepository : Repository<Cupom>, ICupomRepository
    {
        private readonly RubyGameStoreDbContext _dbContext;

        public CupomRepository(RubyGameStoreDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}

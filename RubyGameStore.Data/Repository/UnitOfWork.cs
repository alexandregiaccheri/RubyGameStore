using RubyGameStore.Data.Data;
using RubyGameStore.Data.Repository.IRepository;

namespace RubyGameStore.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RubyGameStoreDbContext dbContext;

        public IPlataformaRepository PlataformaRepo { get; private set; }

        public UnitOfWork(RubyGameStoreDbContext context)
        {
            dbContext = context;
            PlataformaRepo = new PlataformaRepository(dbContext);
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }
    }
}

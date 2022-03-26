using RubyGameStore.Data.Data;
using RubyGameStore.Data.Repository.IRepository;

namespace RubyGameStore.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RubyGameStoreDbContext dbContext;

        public ICategoriaRepository CategoriaRepo { get; private set; }

        public IPlataformaRepository PlataformaRepo { get; private set; }

        public IProdutoRepository ProdutoRepo { get; private set; }

        public UnitOfWork(RubyGameStoreDbContext context)
        {
            dbContext = context;
            CategoriaRepo = new CategoriaRepository(dbContext);
            PlataformaRepo = new PlataformaRepository(dbContext);
            ProdutoRepo = new ProdutoRepository(dbContext);
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }
    }
}

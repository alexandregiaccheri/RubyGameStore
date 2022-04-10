using RubyGameStore.Data.Data;
using RubyGameStore.Data.Repository.IRepository;

namespace RubyGameStore.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RubyGameStoreDbContext dbContext;

        public IProdutoRepository ProdutoRepo { get; private set; }
        public IEmpresaRepository EmpresaRepo { get; private set; }
        public IUsuarioRepository UsuarioRepo { get; private set; }
        public ICarrinhoRepository CarrinhoRepo { get; private set; }
        public IPedidoCabecalhoRepository PedidoCabecalhoRepo { get; private set; }
        public IPedidoDetalhesRepository PedidoDetalhesRepo { get; private set; }

        public UnitOfWork(RubyGameStoreDbContext context)
        {
            dbContext = context;
            ProdutoRepo = new ProdutoRepository(dbContext);
            EmpresaRepo = new EmpresaRepository(dbContext);
            UsuarioRepo = new UsuarioRepository(dbContext);
            CarrinhoRepo = new CarrinhoRepository(dbContext);
            PedidoCabecalhoRepo = new PedidoCabecalhoRepository(dbContext);
            PedidoDetalhesRepo = new PedidoDetalhesRepository(dbContext);
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }

    }
}

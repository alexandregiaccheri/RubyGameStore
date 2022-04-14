using RubyGameStore.Data.Data;
using RubyGameStore.Data.Repository.IRepository;

namespace RubyGameStore.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RubyGameStoreDbContext _dbContext;

        public ICarrinhoRepository CarrinhoRepo { get; private set; }
        public ICupomRepository CupomRepo { get; private set; }
        public IEmpresaRepository EmpresaRepo { get; private set; }
        public IPedidoCabecalhoRepository PedidoCabecalhoRepo { get; private set; }
        public IPedidoDetalhesRepository PedidoDetalhesRepo { get; private set; }
        public IProdutoRepository ProdutoRepo { get; private set; }
        public IUsuarioRepository UsuarioRepo { get; private set; }

        public UnitOfWork(RubyGameStoreDbContext dbContext)
        {
            _dbContext = dbContext;
            CarrinhoRepo = new CarrinhoRepository(_dbContext);
            CupomRepo = new CupomRepository(_dbContext);
            EmpresaRepo = new EmpresaRepository(_dbContext);
            PedidoCabecalhoRepo = new PedidoCabecalhoRepository(_dbContext);
            PedidoDetalhesRepo = new PedidoDetalhesRepository(_dbContext);
            ProdutoRepo = new ProdutoRepository(_dbContext);
            UsuarioRepo = new UsuarioRepository(_dbContext);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

    }
}

namespace RubyGameStore.Data.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IProdutoRepository ProdutoRepo { get; }
        IEmpresaRepository EmpresaRepo { get; }
        IUsuarioRepository UsuarioRepo { get; }
        ICarrinhoRepository CarrinhoRepo { get; }
        IPedidoCabecalhoRepository PedidoCabecalhoRepo { get; }
        IPedidoDetalhesRepository PedidoDetalhesRepo { get; }

        void Save();

    }
}

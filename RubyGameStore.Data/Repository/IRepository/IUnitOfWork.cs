namespace RubyGameStore.Data.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICarrinhoRepository CarrinhoRepo { get; }
        ICupomRepository CupomRepo { get; }
        IEmpresaRepository EmpresaRepo { get; }
        IPedidoCabecalhoRepository PedidoCabecalhoRepo { get; }
        IPedidoDetalhesRepository PedidoDetalhesRepo { get; }
        IProdutoRepository ProdutoRepo { get; }
        IUsuarioRepository UsuarioRepo { get; }

        void Save();

    }
}

namespace RubyGameStore.Data.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IPlataformaRepository PlataformaRepo { get; }
        ICategoriaRepository CategoriaRepo { get; }
        IProdutoRepository ProdutoRepo { get; }
        IEmpresaRepository EmpresaRepo { get; }
        IUsuarioRepository UsuarioRepo { get; }
        ICarrinhoRepository CarrinhoRepo { get; }

        void Save();

    }
}

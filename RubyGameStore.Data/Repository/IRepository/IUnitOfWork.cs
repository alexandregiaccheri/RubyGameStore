namespace RubyGameStore.Data.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IPlataformaRepository PlataformaRepo { get; }

        ICategoriaRepository CategoriaRepo { get; }

        IProdutoRepository ProdutoRepo { get; }

        void Save();
    }
}

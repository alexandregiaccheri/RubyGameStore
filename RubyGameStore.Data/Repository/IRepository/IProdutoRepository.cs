using RubyGameStore.Models.Models;

namespace RubyGameStore.Data.Repository.IRepository
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        void Update(Produto produto);
    }
}

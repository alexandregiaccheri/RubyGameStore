using RubyGameStore.Models.Models;

namespace RubyGameStore.Data.Repository.IRepository
{
    public interface ICarrinhoRepository : IRepository<Carrinho>
    {
        void AdicionarAoCarrinho(Carrinho carrinho, int quantidade);
        void RemoverDoCarrinho(Carrinho carrinho, int quantidade);
    }
}

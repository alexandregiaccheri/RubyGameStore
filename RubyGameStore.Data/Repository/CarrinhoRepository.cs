using RubyGameStore.Data.Data;
using RubyGameStore.Data.Repository.IRepository;
using RubyGameStore.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubyGameStore.Data.Repository
{
    public class CarrinhoRepository : Repository<Carrinho>, ICarrinhoRepository
    {
        private readonly RubyGameStoreDbContext dbContext;
        public CarrinhoRepository(RubyGameStoreDbContext context) : base(context)
        {
            dbContext = context;
        }

        public void AdicionarAoCarrinho(Carrinho carrinho, int quantidade)
        {
            carrinho.Quantidade += quantidade;
        }

        public void RemoverDoCarrinho(Carrinho carrinho, int quantidade)
        {
            carrinho.Quantidade -= quantidade;
        }
    }
}

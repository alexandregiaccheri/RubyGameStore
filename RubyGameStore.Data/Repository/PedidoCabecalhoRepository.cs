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
    public class PedidoCabecalhoRepository : Repository<PedidoCabecalho>, IPedidoCabecalhoRepository
    {
        private readonly RubyGameStoreDbContext dbContext;
        public PedidoCabecalhoRepository(RubyGameStoreDbContext context) : base(context)
        {
            context = dbContext;
        }

        public void AtualizarStatus(int id, string statusPedido, string? statusPagamento = null)
        {
            var pedidoDB = dbContext.PedidosCabecalho.FirstOrDefault(p => p.Id == id);
            if (pedidoDB != null)
            {
                pedidoDB.StatusPedido = statusPedido;
                if (statusPagamento != null)
                {
                    pedidoDB.StatusPagamento = statusPagamento;
                }
            }
        }

        public void Update(PedidoCabecalho pedidoCabecalho)
        {
            dbContext.PedidosCabecalho.Update(pedidoCabecalho);
        }
    }
}

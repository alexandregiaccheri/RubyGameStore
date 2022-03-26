using RubyGameStore.Data.Data;
using RubyGameStore.Data.Repository.IRepository;
using RubyGameStore.Models.Models;

namespace RubyGameStore.Data.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        private readonly RubyGameStoreDbContext dbContext;

        public ProdutoRepository(RubyGameStoreDbContext context) : base(context)
        {
            dbContext = context;
        }

        public void Update(Produto obj)
        {
            var queryObj = dbContext.Produtos.FirstOrDefault(o => o.Id == obj.Id);
            if (queryObj != null)
            {
                queryObj.Titulo = obj.Titulo;
                queryObj.Descricao = obj.Descricao;
                queryObj.Desenvolvedor = obj.Desenvolvedor;
                queryObj.Distribuidor = obj.Distribuidor;
                queryObj.PrecoListado = obj.PrecoListado;
                queryObj.Preco = obj.Preco;
                queryObj.Preco50 = obj.Preco50;
                queryObj.Preco100 = obj.Preco100;
                queryObj.CategoriaId = obj.CategoriaId;
                queryObj.PlataformaId = obj.PlataformaId;
                if (obj.ImgCapaUrl != null)
                {
                    queryObj.ImgCapaUrl = obj.ImgCapaUrl;
                }
            }
            
        }
    }
}

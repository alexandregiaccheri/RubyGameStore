using RubyGameStore.Data.Data;
using RubyGameStore.Data.Repository.IRepository;
using RubyGameStore.Models.Models;

namespace RubyGameStore.Data.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        private readonly RubyGameStoreDbContext _dbContext;

        public ProdutoRepository(RubyGameStoreDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update(Produto obj)
        {
            var queryObj = _dbContext.Produtos.FirstOrDefault(o => o.Id == obj.Id);
            if (queryObj != null)
            {
                queryObj.Classificacao = obj.Classificacao;
                queryObj.DataLancamento = obj.DataLancamento;
                queryObj.Desenvolvedor = obj.Desenvolvedor;
                queryObj.Descricao = obj.Descricao;
                queryObj.Distribuidor = obj.Distribuidor;
                queryObj.Genero = obj.Genero;
                queryObj.Jogadores = obj.Jogadores;
                queryObj.LinkTrailer = obj.LinkTrailer;
                queryObj.Metascore = obj.Metascore;
                queryObj.Plataforma = obj.Plataforma;
                queryObj.PrecoNormal = obj.PrecoNormal;
                queryObj.PrecoPromo = obj.PrecoPromo;                
                queryObj.Titulo = obj.Titulo;

                if (obj.BoxArt != null)
                {
                    queryObj.BoxArt = obj.BoxArt;
                }

                if (obj.Screenshot1 != null)
                {
                    queryObj.Screenshot1 = obj.Screenshot1;
                }

                if (obj.Screenshot2 != null)
                {
                    queryObj.Screenshot2 = obj.Screenshot2;
                }

                if (obj.Screenshot3 != null)
                {
                    queryObj.Screenshot3 = obj.Screenshot3;
                }

                if (obj.Screenshot4 != null)
                {
                    queryObj.Screenshot4 = obj.Screenshot4;
                }

                if (obj.Screenshot5 != null)
                {
                    queryObj.Screenshot5 = obj.Screenshot5;
                }

                if (obj.Screenshot6 != null)
                {
                    queryObj.Screenshot6 = obj.Screenshot6;
                }

            }
        }
    }
}

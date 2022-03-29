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
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        private readonly RubyGameStoreDbContext dbContext;
        public UsuarioRepository(RubyGameStoreDbContext context) : base(context)
        {
            dbContext = context;
        }
    }
}

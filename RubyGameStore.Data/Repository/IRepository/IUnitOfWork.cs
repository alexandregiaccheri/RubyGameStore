using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubyGameStore.Data.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IPlataformaRepository PlataformaRepo { get; }

        void Save();
    }
}

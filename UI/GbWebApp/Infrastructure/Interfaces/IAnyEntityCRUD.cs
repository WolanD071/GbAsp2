using GbWebApp.Domain.Entities.Base.Interfaces;
using System.Linq;

namespace GbWebApp.Infrastructure.Interfaces
{
    public interface IAnyEntityCRUD<T> where T : class, IEntity
    {
        IQueryable<T> Get();
        T Get(int id);
        int Add(T emp);
        void Update(T emp);
        bool Delete(int id);
    }
}

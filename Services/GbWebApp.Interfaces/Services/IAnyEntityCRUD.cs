using System.Collections.Generic;
using GbWebApp.Domain.Entities.Base.Interfaces;

namespace GbWebApp.Interfaces.Services
{
    public interface IAnyEntityCRUD<T> where T : class, IEntity
    {
        IEnumerable<T> Get();
        T Get(int id);
        int Add(T entity);
        void Update(T entity);
        bool Delete(int id);
    }
}

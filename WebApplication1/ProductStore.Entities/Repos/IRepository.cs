using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductStore.Entities.Repos
{
    public interface IRepository<T>
        where T : class
    {
        Task<IEnumerable<T>> GetAsync();
        Task<T> GetByIdAsync(int id);
        void Create(T item);
        void Update(T item);
        void Delete(T item);
    }
}

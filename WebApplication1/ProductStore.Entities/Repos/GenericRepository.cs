using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductStore.Entities.Repos
{
    public class GenericRepository<T> : IRepository<T>
        where T : class
    {
        private readonly ApplicationContext _context;
        public GenericRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public void Create(T item)
        {
            _context.Set<T>().Add(item);
            _context.SaveChanges();
        }
        public void Update(T item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public void Delete(T item)
        {
            _context.Set<T>().Remove(item);
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            T entityToDelete = _context.Set<T>().Find(id);
            Delete(entityToDelete);
        }       
    }
}

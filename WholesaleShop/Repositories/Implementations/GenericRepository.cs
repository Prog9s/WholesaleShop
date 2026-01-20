using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WholesaleShop.Data;
using WholesaleShop.Repositories.Interfaces;

namespace WholesaleShop.Repositories.Implementations
{

    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        // Injection App => _context
        private readonly ApplicationDbContext _context;

        // Add include
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            //return _context.Set<T>().ToList();
            return _dbSet.ToList();
        }
        // Implementation for eager loading
        public IEnumerable<T> GetAll(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return query.ToList();
        }
        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public T GetByUid(string Uid)
        {
            return _context.Set<T>().SingleOrDefault(e => EF.Property<string>(e, "Uid") == Uid);
        }

        //Add 
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        //Edit
        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);

        }

        //Delete
        public void Delete(int id)
        {
            var entity = _context.Set<T>().Find(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                //_context.SaveChanges();
            }
        }


    }

}

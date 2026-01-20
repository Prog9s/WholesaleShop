using System.Linq.Expressions;

namespace WholesaleShop.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(params Expression<Func<T, object>>[] includes);
        T GetById(int id);
        T GetByUid(string Uid);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
    }

}

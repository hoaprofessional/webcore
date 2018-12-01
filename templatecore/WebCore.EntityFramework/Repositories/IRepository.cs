using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebCore.Utils.ModelHelper;

namespace WebCore.EntityFramework.Repositories
{
    public interface IRepository<TModel, TKey> where TModel : class, IAuditable<TKey>
    {
        Task<TModel> AddAsync(TModel entity);
        TModel Add(TModel entity);

        void Update(TModel entity);

        void Delete(TModel entity);
        void Delete(TKey id);

        Task<TModel> GetByIdAsync(TKey id);
        TModel GetById(TKey id);

        IQueryable<TModel> GetByCondition(Expression<Func<TModel, bool>> predicate);
        IQueryable<TModel> GetAll();

        TModel GetFirstByCondition(Expression<Func<TModel, bool>> predicate);

        Task<decimal> SumAsync(Expression<Func<TModel, decimal>> selector);
        decimal Sum(Expression<Func<TModel, decimal>> selector);

    }
}

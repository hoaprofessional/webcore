using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebCore.EntityFramework.Data;
using WebCore.Utils.ModelHelper;

namespace WebCore.EntityFramework.Repositories
{
    public class RepositoryImpl<TModel, TKey> : IRepository<TModel, TKey> where TModel : class, IAuditable<TKey>
    {
        private readonly WebCoreDbContext dbContext;
        public RepositoryImpl(WebCoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        protected DbSet<TModel> DataSet => dbContext.Set<TModel>();

        public TModel Add(TModel entity)
        {
            return DataSet.Add(entity).Entity;
        }

        public async Task<TModel> AddAsync(TModel entity)
        {
            var entityResult = await DataSet.AddAsync(entity);
            return entityResult.Entity;
        }

        public void Delete(TModel entity)
        {
            DataSet.Remove(entity);
        }

        public void Delete(TKey id)
        {
            DataSet.Remove(GetById(id));
        }

        public IQueryable<TModel> GetAll()
        {
            return DataSet;
        }

        public IQueryable<TModel> GetByCondition(Expression<Func<TModel, bool>> predicate)
        {
            return DataSet.Where(predicate);
        }

        public TModel GetFirstByCondition(Expression<Func<TModel, bool>> predicate)
        {
            return GetByCondition(predicate).FirstOrDefault();
        }


        public TModel GetById(TKey id)
        {
            return DataSet.Find(id);
        }

        public Task<TModel> GetByIdAsync(TKey id)
        {
            return DataSet.FindAsync(id);
        }

        public decimal Sum(Expression<Func<TModel, decimal>> selector)
        {
            return DataSet.Sum(selector);
        }

        public Task<decimal> SumAsync(Expression<Func<TModel, decimal>> selector)
        {
            return DataSet.SumAsync(selector);
        }

        public void Update(TModel entity)
        {
            DataSet.Update(entity);
        }
    }
}

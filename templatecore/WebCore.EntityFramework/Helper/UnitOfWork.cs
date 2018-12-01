using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebCore.EntityFramework.Data;

namespace WebCore.EntityFramework.Helper
{
    public interface IUnitOfWork
    {
        void BeginTransaction();
        void CommitTransaction();
        int SaveChanges(bool acceptAllChangesOnSuccess);
        int SaveChanges();
        Task SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        void Rollback();
    }
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WebCoreDbContext dbContext;
        private IDbContextTransaction dbTransaction;
        public UnitOfWork(WebCoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void BeginTransaction()
        {
            dbTransaction = dbContext.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            if(dbTransaction!=null)
            {
                dbTransaction.Commit();
                dbTransaction = null;
            }
        }

        public void Rollback()
        {
            dbContext.Database.RollbackTransaction();
        }

        public int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            return dbContext.SaveChanges(acceptAllChangesOnSuccess);
        }
        public int SaveChanges()
        {
            return dbContext.SaveChanges();
        }
        public Task SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}

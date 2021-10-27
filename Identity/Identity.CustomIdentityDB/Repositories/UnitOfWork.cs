using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;

namespace Identity.CustomIdentityDb.Repository
{
    public class UnitOfWork<C> : IUnitOfWork<C>, IAsyncDisposable where C : DbContext
    {
        private IDbContextTransaction _transaction;
        private bool _dispose;

        public C DbContext { get; set; }

        public UnitOfWork(C dbContext)
        {
            this.DbContext = dbContext;
        }

        public async Task CreateTransactionAsync()
        {
            this._transaction = await DbContext.Database.BeginTransactionAsync();
        }
        public async Task CommitAsync()
        {
            await this._transaction.CommitAsync();
        }

        public async Task RollbackAsync()
        {
            await this._transaction.RollbackAsync();
            this._transaction.Dispose();
        }

        public async Task DisposeAsync(bool disposing)
        {
            await DisposeAsync(true);
            GC.SuppressFinalize(this);
        }

        public async Task DisposeAsync()
        {
            if (!this._dispose)
            {
                await this.DbContext.DisposeAsync();
                this._dispose = true;
            }
        }

        public async Task SaveChangeAsync()
        {
            await this.DbContext.SaveChangesAsync();
        }

        async ValueTask IAsyncDisposable.DisposeAsync()
        {
            await DisposeAsync(true);
            GC.SuppressFinalize(this);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Identity.CustomIdentityDb.Repository;
using Microsoft.EntityFrameworkCore;

namespace Identity.CustomIdentityDB.Repository
{
    public class GenericRepository<C, T> : IGenericRepository<T>
        where T : class
        where C : DbContext
    {
        protected IUnitOfWork<C> Uow { get; private set; }

        public GenericRepository(IUnitOfWork<C> uow) => this.Uow = uow;

        public async Task<IList<T>> GetAllAsync()
        {
            return await this.Uow.DbContext.Set<T>().ToListAsync();
        }

        public async Task<IList<T>> FindByAsync(Expression<Func<T, bool>> expression)
        {
            return await this.Uow.DbContext.Set<T>().Where(expression).ToListAsync();
        }

        public async Task<T> FindSingleAsync(Expression<Func<T, bool>> expression)
        {
            return await this.Uow.DbContext.Set<T>().SingleOrDefaultAsync(expression);
        }

        public async Task InsertAsync(T entity)
        {
            this.Uow.DbContext.Set<T>().Add(entity);
            await this.Uow.SaveChangeAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            this.Uow.DbContext.Set<T>().Update(entity);
            await this.Uow.SaveChangeAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            this.Uow.DbContext.Set<T>().Remove(entity);
            await this.Uow.SaveChangeAsync();
        }
    }
}
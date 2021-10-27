using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Identity.CustomIdentityDB.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        /// <summary>
        /// Get all data from table
        /// </summary>
        /// <returns>List of records</returns>
        Task<IList<T>> GetAllAsync();

        /// <summary>
        /// Get list of data from table with condition
        /// </summary>
        /// <param name="expression">expression</param>
        /// <returns>List of record</returns>
        Task<IList<T>> FindByAsync(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Get single data from table with condition
        /// </summary>
        /// <param name="expression">expression</param>
        /// <returns>a record</returns>
        Task<T> FindSingleAsync(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Insert an entity to table
        /// </summary>
        /// <param name="entity">T entity</param>
        Task InsertAsync(T entity);

        /// <summary>
        /// Update an entity into a record in table
        /// </summary>
        /// <param name="entity">T entity</param>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Delete a record in table
        /// </summary>
        /// <param name="id">T entity</param>
        Task DeleteAsync(T entity);
    }
}
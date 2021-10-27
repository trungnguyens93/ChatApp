using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Identity.CustomIdentityDb.Repository
{
    public interface IUnitOfWork<C> where C : DbContext
    {
        C DbContext { get; }
        Task CreateTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
        Task SaveChangeAsync();
    }
}
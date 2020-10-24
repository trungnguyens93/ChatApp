using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Identity.Basic.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;

namespace Identity.Basic.Stores
{
    public class UserStore : IUserStore<User>, IUserPasswordStore<User>
    {
        private static DbConnection GetOpenConnection()
        {
            var connection = new SqlConnection("Server=.;Database=IdentityDB;User Id=sa;Password=Admin@123;");
            connection.Open();

            return connection;
        }

        #region UserStore
        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id);
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedUserName);
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            return Task.CompletedTask;
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            return Task.CompletedTask;
        }

        public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            using (var conn = GetOpenConnection())
            {
                return await conn.QueryFirstOrDefaultAsync<User>(
                                    "SELECT * FROM [dbo].[Users] WHERE [Id] = @Id",
                                    new
                                    {
                                        Id = userId
                                    }
                                );
            }
        }

        public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            using (var conn = GetOpenConnection())
            {
                return await conn.QueryFirstOrDefaultAsync<User>(
                    "SELECT * FROM [dbo].[Users] WHERE [NormalizedUserName] = @NormalizedUserName",
                    new
                    {
                        NormalizedUserName = normalizedUserName
                    }
                );
            }
        }

        public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            using (var conn = GetOpenConnection())
            {
                await conn.ExecuteAsync(
                        "INSERT INTO Users" +
                        "(" +
                        "   Id," +
                        "   UserName," +
                        "   NormalizedUserName," +
                        "   PasswordHash" +
                        ")" +
                        "VALUES " +
                        "(" +
                        "   @Id," +
                        "   @UserName," +
                        "   @NormalizedUserName," +
                        "   @PasswordHash" +
                        ")",
                        new
                        {
                            Id = user.Id,
                            UserName = user.UserName,
                            NormalizedUserName = user.NormalizedUserName,
                            PasswordHash = user.PasswordHash
                        });
            }

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            using (var conn = GetOpenConnection())
            {
                await conn.ExecuteAsync(
                        "UPDATE Users" +
                        "SET UserName = @UserName" +
                        "    NormalizedUserName = @NormalizedUserName" +
                        "    PasswordHash = @PasswordHash" +
                        "WHERE Id = @Id",
                        new
                        {
                            Id = user.Id,
                            UserName = user.UserName,
                            NormalizedUserName = user.NormalizedUserName,
                            PasswordHash = user.PasswordHash
                        });
            }

            return IdentityResult.Success;
        }

        public Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        #endregion UserStore


        #region PasswordStore
        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash != null);
        }

        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.CompletedTask;
        }
        #endregion PasswordStore

        public void Dispose()
        {

        }
    }
}
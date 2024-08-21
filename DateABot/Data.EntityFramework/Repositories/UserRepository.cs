using Domain.Shared;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Data.EntityFramework.Repositories
{
    internal sealed class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> AlreadyExists(Email email, CancellationToken cancellationToken = default)
        {
            return await DbContext
                .Set<User>()
                .AnyAsync(user => user.Email == email, cancellationToken);
        }
    }
}

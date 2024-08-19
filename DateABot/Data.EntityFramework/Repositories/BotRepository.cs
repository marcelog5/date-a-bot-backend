using Domain.Bots;

namespace Data.EntityFramework.Repositories
{
    internal sealed class BotRepository : Repository<Bot>, IBotRepository
    {
        public BotRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}

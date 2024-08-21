using Domain.Shared;

namespace Domain.Users
{
    public interface IUserRepository
    {
        Task<User?> GetById(Guid Id, CancellationToken cancellationToken = default);
        Task<bool> AlreadyExists(Email email, CancellationToken cancellationToken = default);
        Task Add(User user, CancellationToken cancellationToken = default);
    }
}

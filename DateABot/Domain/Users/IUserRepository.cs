namespace Domain.Users
{
    public interface IUserRepository
    {
        Task<User?> GetById(Guid Id, CancellationToken cancellationToken = default);
    }
}

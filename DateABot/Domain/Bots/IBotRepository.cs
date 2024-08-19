namespace Domain.Bots
{
    public interface IBotRepository
    {
        Task<Bot?> GetById(Guid Id, CancellationToken cancellationToken = default);
    }
}

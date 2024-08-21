using Domain.Shared;

namespace Application.UseCases.UserUseCases
{
    public sealed record CreateUserInput(
        Name Name,
        Email Email,
        string Password);
}

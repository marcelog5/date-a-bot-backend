using Application.Abstracts;
using Domain.Abstracts;

namespace Application.UseCases.UserUseCases
{
    public interface ICreateUserUseCase : IUseCase<CreateUserInput, Result<CreateUserOutput>>
    {
    }
}

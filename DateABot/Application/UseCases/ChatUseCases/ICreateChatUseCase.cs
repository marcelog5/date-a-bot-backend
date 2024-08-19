using Application.Abstracts;
using Domain.Abstracts;

namespace Application.UseCases.ChatUseCases
{
    public interface ICreateChatUseCase : IUseCase<CreateChatInput, Result<CreateChatOutput>>
    {
    }
}

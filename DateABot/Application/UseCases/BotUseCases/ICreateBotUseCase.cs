using Application.Abstracts;
using Domain.Abstracts;

namespace Application.UseCases.BotUseCases
{
    public interface ICreateBotUseCase : IUseCase<CreateBotInput, Result<CreateBotOutput>>
    {
    }
}

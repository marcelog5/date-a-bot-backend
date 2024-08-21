using Domain.Abstracts;
using Domain.Bots;

namespace Application.UseCases.BotUseCases
{
    public sealed class CreateBotUseCase : ICreateBotUseCase
    {
        private readonly IBotRepository _botRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateBotUseCase
        (
            IBotRepository botRepository,
            IUnitOfWork unitOfWork
        )
        {
            _botRepository = botRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<CreateBotOutput>> Execute(
            CreateBotInput input, 
            CancellationToken cancellationToken = default)
        {
            Bot bot = new Bot(
                input.Name,
                input.Role,
                input.Goal,
                input.Backstory,
                input.Avatar);

            await _botRepository.Add(bot);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new CreateBotOutput(bot.Id);
        }
    }
}

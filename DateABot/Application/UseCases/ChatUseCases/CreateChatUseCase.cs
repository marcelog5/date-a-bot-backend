using Domain.Abstracts;
using Domain.Bots;
using Domain.Chats;
using Domain.Users;

namespace Application.UseCases.ChatUseCases
{
    public sealed class CreateChatUseCase : ICreateChatUseCase
    {
        private readonly IBotRepository _botRepository;
        private readonly IUserRepository _userRepository;
        private readonly IChatRepository _chatRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateChatUseCase(
            IBotRepository botRepository, 
            IUserRepository userRepository, 
            IChatRepository chatRepository, 
            IUnitOfWork unitOfWork)
        {
            _botRepository = botRepository;
            _userRepository = userRepository;
            _chatRepository = chatRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<CreateChatOutput>> Execute(
            CreateChatInput input, 
            CancellationToken cancellationToken = default)
        {
            Bot bot = await _botRepository.GetById(input.BotId, cancellationToken);

            if (bot is null)
            {
                return Result.Failure<CreateChatOutput>(BotErrors.NotFound);
            }

            User user = await _userRepository.GetById(input.UserId, cancellationToken);

            if (user is null)
            {
                return Result.Failure<CreateChatOutput>(UserErrors.NotFound);
            }

            bool chatExists = await _chatRepository.Exist(input.BotId, input.UserId, cancellationToken);

            if (chatExists)
            {
                return Result.Failure<CreateChatOutput>(ChatErrors.AlreadyExist);
            }

            Chat chat = new Chat(bot.Id, user.Id);

            await _chatRepository.Add(chat, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new CreateChatOutput();
        }
    }
}

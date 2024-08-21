using Domain.Abstracts;
using Domain.Users;

namespace Application.UseCases.UserUseCases
{
    public sealed class CreateUserUseCase : ICreateUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;

        public CreateUserUseCase
        (
            IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            IPasswordHasher passwordHasher
        )
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }

        public async Task<Result<CreateUserOutput>> Execute(
            CreateUserInput input,
            CancellationToken cancellationToken = default)
        {
            bool userExists = await _userRepository.AlreadyExists(input.Email, cancellationToken);

            if (userExists)
            {
                return Result.Failure<CreateUserOutput>(UserErrors.AlreadyExists);
            }

            // Hash the password
            string hashedPassword = _passwordHasher.HashPassword(input.Password);

            User user = new User
            (
                input.Name,
                input.Email,
                hashedPassword
            );

            await _userRepository.Add(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new CreateUserOutput
            (
                user.Id
            );
        }
    }
}

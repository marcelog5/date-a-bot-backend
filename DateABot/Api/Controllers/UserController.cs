using Application.UseCases.UserUseCases;
using Domain.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ICreateUserUseCase _createUserUseCase;

        public UserController
        (
            ICreateUserUseCase createUserUseCase
        )
        {
            _createUserUseCase = createUserUseCase;
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<Result<CreateUserOutput>> Post(
            [FromBody] CreateUserInput input,
            CancellationToken cancellationToken)
        {
            return await _createUserUseCase.Execute(input, cancellationToken);
        }
    }
}

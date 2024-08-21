using Application.UseCases.BotUseCases;
using Domain.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BotController : ControllerBase
    {
        private readonly ICreateBotUseCase _createBotUseCase;

        public BotController
        (
            ICreateBotUseCase createBotUseCase
        )
        {
            _createBotUseCase = createBotUseCase;
        }

        // POST api/<BotController>
        [HttpPost]
        public async Task<Result<CreateBotOutput>> Post(
            [FromBody] CreateBotInput input,
            CancellationToken cancellationToken)
        {
            return await _createBotUseCase.Execute(input, cancellationToken);
        }
    }
}

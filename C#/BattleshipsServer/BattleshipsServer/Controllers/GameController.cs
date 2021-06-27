using System.Linq;
using System.Threading.Tasks;
using BattleshipsServer.Helpers;
using BattleshipsServer.Interfaces;
using BattleshipsServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BattleshipsServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IGameContext _gameContext;
        private readonly ILogger<GameController> _logger;
        private readonly IProcessor<GameParticipant> _processor;
        private readonly IValidator _validator;


        public GameController(
            IGameContext gameContext,
            ILogger<GameController> logger,
            IProcessor<GameParticipant> processor,
            IValidator validator)
        {
            Verify.NotNull(gameContext, nameof(gameContext));
            Verify.NotNull(logger, nameof(logger));
            Verify.NotNull(processor, nameof(processor));
            Verify.NotNull(validator, nameof(validator));

            _gameContext = gameContext;
            _logger = logger;
            _processor = processor;
            _validator = validator;
        }

        [HttpPost]
        public async Task<IActionResult> BeginGame()
        {
            await _gameContext.BeginGame();

            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpPost]
        public IActionResult Join(GameParticipant participant)
        {
            if (!_gameContext.IsGameInProgress())
            {
                return StatusCode(
                    StatusCodes.Status409Conflict,
                    new { errors = new[] {"The game has not yet begun"} }
                );
            }

            var validatorResult = _validator.Validate(participant);
            if (!validatorResult.IsValid)
            {
                return StatusCode(
                    StatusCodes.Status400BadRequest,
                    new { errors = validatorResult.Errors.ToArray() }
                );
            }

            _processor.Process(participant);

            var response = new JoinResponse
            {
                GameSettings = _gameContext.GetGameSettings()
            };

            return new JsonResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> EndGame()
        {
            await _gameContext.EndGame();

            return StatusCode(StatusCodes.Status200OK);
        }
    }
}

using BattleshipsServer.Helpers;
using BattleshipsServer.Interfaces;
using BattleshipsServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace BattleshipsServer.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TournamentController : ControllerBase
    {
        private readonly ILogger<TournamentController> _logger;
        private readonly IProcessor<Participant> _participantProcessor;
        private readonly ITournamentContext _tournamentContext;
        private readonly IValidator _validator;


        public TournamentController(
            ILogger<TournamentController> logger,
            IProcessor<Participant> participantProcessor,
            ITournamentContext tournamentContext,
            IValidator validator)
        {
            Verify.NotNull(logger, nameof(logger));
            Verify.NotNull(participantProcessor, nameof(participantProcessor));
            Verify.NotNull(tournamentContext, nameof(tournamentContext));
            Verify.NotNull(validator, nameof(validator));

            _logger = logger;
            _participantProcessor = participantProcessor;
            _tournamentContext = tournamentContext;
            _validator = validator;
        }

        [HttpPost]
        public IActionResult Begin()
        {
            _tournamentContext.Begin();

            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpPost]
        public IActionResult Register([FromBody] Participant participant)
        {
            if (_tournamentContext.GetTournamentSettings() == null)
            {
                return StatusCode(
                    StatusCodes.Status409Conflict,
                    new { errors = new[] { "The tournament has not yet begun"} }
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

            _participantProcessor.Process(participant);

            return StatusCode(
                StatusCodes.Status200OK,
                new { messsage = "You have been registered as a participant in the Battleships tournament." }
            );
        }

        [HttpPost]
        public IActionResult End()
        {
            _tournamentContext.End();

            return StatusCode(StatusCodes.Status200OK);
        }
    }
}

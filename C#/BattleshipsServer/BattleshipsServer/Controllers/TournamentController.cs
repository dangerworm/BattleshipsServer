using BattleshipsServer.Enums;
using BattleshipsServer.Helpers;
using BattleshipsServer.Interfaces;
using BattleshipsServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BattleshipsServer.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TournamentController : ControllerBase
    {
        private readonly ILogger<TournamentController> _logger;
        private readonly ITournamentContext _tournamentContext;

        public TournamentController(
            ILogger<TournamentController> logger,
            ITournamentContext tournamentContext)
        {
            Verify.NotNull(logger, nameof(logger));
            Verify.NotNull(tournamentContext, nameof(tournamentContext));

            _logger = logger;
            _tournamentContext = tournamentContext;
        }

        [HttpPost]
        public IActionResult CreateNew()
        {
            var tournamentSettings = _tournamentContext.CreateNew();

            return StatusCode(StatusCodes.Status200OK, new { tournamentSettings });
        }

        [HttpPost]
        public Task<IActionResult> Register([FromBody] Participant participant)
        {
            return HandleRegistration(participant, ProcessorOperation.Add);
        }

        [HttpPost]
        public Task<IActionResult> EditRegistration([FromBody] Participant participant)
        {
            return HandleRegistration(participant, ProcessorOperation.Edit);
        }

        [HttpPost]
        public IActionResult Begin()
        {
            try
            {
                _tournamentContext.Begin();
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status412PreconditionFailed, new
                    {
                        success = false,
                        message = exception.Message
                    }
                );

            }
        }

        [HttpPost]
        public IActionResult End()
        {
            try
            {
                _tournamentContext.End();
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status412PreconditionFailed, new
                    {
                        success = false,
                        message = exception.Message
                    }
                );

            }
        }

        private async Task<IActionResult> HandleRegistration(Participant participant, ProcessorOperation operation)
        {
            if (_tournamentContext.GetTournamentSettings() == null)
            {
                return StatusCode(
                    StatusCodes.Status409Conflict,
                    new { errors = new[] { "The tournament has not yet begun" } }
                );
            }

            var result = await _tournamentContext.ProcessParticipant(participant, operation);

            if (!result.ValidatorResult.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                    {
                        message = result.Message,
                        errors = result.ValidatorResult.Errors.ToArray()
                    }
                );
            }

            if (result.IsSuccess)
            {
                return StatusCode(StatusCodes.Status200OK, new
                {
                    success = result.IsSuccess,
                    tournamentSettings = _tournamentContext.GetTournamentSettings()
                });
            }

            switch (operation)
            {
                case ProcessorOperation.Add:
                    return StatusCode(StatusCodes.Status409Conflict, new
                    {
                        message = result.Message
                    });
                case ProcessorOperation.Edit:
                    return StatusCode(StatusCodes.Status422UnprocessableEntity, new
                    {
                        message = result.Message,
                        tournamentSettings = _tournamentContext.GetTournamentSettings()
                    });
                default:
                    throw new InvalidOperationException();
            };
        }
    }
}

using BattleshipsServer.Controllers;
using BattleshipsServer.Interfaces;
using BattleshipsServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace BattleshipsServerTests.ControllerTests
{
    public class TournamentControllerTests : ControllerTestsBase
    {
        private Mock<ILogger<TournamentController>> _mockLogger;
        private Mock<ITournamentContext> _mockTournamentContext;

        [SetUp]
        public void Setup()
        {
            MockParticipantProcessor = new Mock<IProcessor<Participant>>();
            MockValidator = new Mock<IValidator>();

            _mockLogger = new Mock<ILogger<TournamentController>>();
            _mockTournamentContext = new Mock<ITournamentContext>();
        }

        [Test]
        public void CreateNewCallsCreateNewOnTournamentContext()
        {
            var tournamentController = new TournamentController(_mockLogger.Object, MockParticipantProcessor.Object, _mockTournamentContext.Object, MockValidator.Object);

            var result = tournamentController.CreateNew();

            _mockTournamentContext.Verify(x => x.CreateNew(), Times.Once());
            AssertHttpCode(result, StatusCodes.Status200OK);
        }

        [Test]
        public void RegisterReturns409IfTournamentHasNotBegun()
        {
            _mockTournamentContext
                .Setup(x => x.GetTournamentSettings())
                .Returns(default(TournamentSettings));

            var tournamentController = new TournamentController(_mockLogger.Object, MockParticipantProcessor.Object, _mockTournamentContext.Object, MockValidator.Object);

            var result = tournamentController.Register(new Participant());

            AssertHttpCode(result, StatusCodes.Status409Conflict);
        }

        [Test]
        public void RegisterReturns400IfParticipantIsInvalid()
        {
            _mockTournamentContext
                .Setup(x => x.GetTournamentSettings())
                .Returns(new TournamentSettings());

            MockValidator
                .Setup(x => x.Validate(It.IsAny<Participant>()))
                .Returns(new ValidatorResult
                {
                    Errors = new[] {"Error"}
                });


            var tournamentController = new TournamentController(_mockLogger.Object, MockParticipantProcessor.Object, _mockTournamentContext.Object, MockValidator.Object);

            var result = tournamentController.Register(new Participant());

            AssertHttpCode(result, StatusCodes.Status400BadRequest);
        }


        [Test]
        public void EndCallsEndOnTournamentContext()
        {
            var tournamentController = new TournamentController(_mockLogger.Object, MockParticipantProcessor.Object, _mockTournamentContext.Object, MockValidator.Object);

            var result = tournamentController.End();

            _mockTournamentContext.Verify(x => x.End(), Times.Once());
            AssertHttpCode(result, StatusCodes.Status200OK);
        }
    }
}

using BattleshipsServer.Controllers;
using BattleshipsServer.Interfaces;
using BattleshipsServer.Models;
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
        public void BeginCallsBeginOnTournamentContext()
        {
            var tournamentController = new TournamentController(_mockLogger.Object, MockParticipantProcessor.Object, _mockTournamentContext.Object, MockValidator.Object);

            tournamentController.Begin();

            _mockTournamentContext.Verify(x => x.Begin(), Times.Once());
        }

        [Test]
        public void EndCallsEndOnTournamentContext()
        {
            var tournamentController = new TournamentController(_mockLogger.Object, MockParticipantProcessor.Object, _mockTournamentContext.Object, MockValidator.Object);

            tournamentController.End();

            _mockTournamentContext.Verify(x => x.End(), Times.Once());
        }
    }
}

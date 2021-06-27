using BattleshipsServer.Controllers;
using BattleshipsServer.Interfaces;
using BattleshipsServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace BattleshipsServerTests.ControllerTests
{
    public class GameControllerTests
    {
        private Mock<IGameContext> _mockGameContext;
        private Mock<ILogger<GameController>> _mockLogger;
        private Mock<IProcessor<GameParticipant>> _mockProcessor;
        private Mock<IValidator> _mockValidator;

        [SetUp]
        public void Setup()
        {
            _mockGameContext = new Mock<IGameContext>();
            _mockLogger = new Mock<ILogger<GameController>>();
            _mockProcessor = new Mock<IProcessor<GameParticipant>>();
            _mockValidator = new Mock<IValidator>();
        }

        [Test]
        public void JoinReturnsJsonResult()
        {
            _mockGameContext.Setup(x => x.IsGameInProgress()).Returns(true);

            SetupValidatorErrors(new string[0]);

            var gameController = new GameController(_mockGameContext.Object, _mockLogger.Object, _mockProcessor.Object, _mockValidator.Object);
            var gameParticipant = new GameParticipant();

            var response = gameController.Join(gameParticipant);

            Assert.That(response.GetType() == typeof(JsonResult));
        }

        [Test]
        public void JoinReturns409IfNoGameInProgress()
        {
            _mockGameContext.Setup(x => x.IsGameInProgress()).Returns(false);

            var gameController = new GameController(_mockGameContext.Object, _mockLogger.Object, _mockProcessor.Object, _mockValidator.Object);
            var result = gameController.Join(new GameParticipant());

            AssertHttpCode(result, StatusCodes.Status409Conflict);
        }


        [Test]
        public void JoinReturns400IfGameParticipantIsInvalid()
        {
            _mockGameContext.Setup(x => x.IsGameInProgress()).Returns(true);

            SetupValidatorErrors(new [] {"Error"});

            var gameController = new GameController(_mockGameContext.Object, _mockLogger.Object, _mockProcessor.Object, _mockValidator.Object);
            var result = gameController.Join(new GameParticipant());

            AssertHttpCode(result, StatusCodes.Status400BadRequest);
        }

        private void SetupValidatorErrors(string[] errors)
        {
            _mockValidator
                .Setup(x => x.Validate(It.IsAny<GameParticipant>()))
                .Returns(new ValidatorResult
                {
                    Errors = new List<string>(errors)
                });
        }

        private void AssertHttpCode(IActionResult result, int statusCode)
        {
            Assert.That(result.GetType() == typeof(ObjectResult));
            Assert.That(((ObjectResult)result).StatusCode == statusCode);
        }
    }
}

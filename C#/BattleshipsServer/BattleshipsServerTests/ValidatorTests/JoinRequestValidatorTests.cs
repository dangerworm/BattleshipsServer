using BattleshipsServer.Validators;
using NUnit.Framework;
using System.Linq;
using BattleshipsServer.Models;

namespace BattleshipsServerTests.ValidatorTests
{
    public class GameParticipantValidatorTests
    {
        private GameParticipantValidator _GameParticipantValidator;

        [SetUp]
        public void Setup()
        {
            _GameParticipantValidator = new GameParticipantValidator();
        }

        [Test]
        public void GameParticipantValidatorReturnsInvalidIfGameParticipantIsNull()
        {
            var result = _GameParticipantValidator.Validate(null);

            Assert.Greater(result.Errors.Count(e => e.Contains("null")), 0);
            Assert.AreEqual(result.IsValid, false);
        }

        [Test]
        public void GameParticipantValidatorReturnsInvalidIfGameParticipantNameIsNullOrEmpty()
        {
            var GameParticipant = new GameParticipant
            {
                Name = ""
            };

            var result = _GameParticipantValidator.Validate(GameParticipant);

            Assert.Greater(result.Errors.Count(e => e.Contains("Name")), 0);
            Assert.False(result.IsValid);
        }
    }
}

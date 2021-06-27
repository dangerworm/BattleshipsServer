using BattleshipsServer.Validators;
using NUnit.Framework;
using System.Linq;
using BattleshipsServer.Models;

namespace BattleshipsServerTests.ValidatorTests
{
    public class ParticipantValidatorTests
    {
        private ParticipantValidator _participantValidator;

        [SetUp]
        public void Setup()
        {
            _participantValidator = new ParticipantValidator();
        }

        [Test]
        public void ParticipantValidatorReturnsInvalidIfParticipantIsNull()
        {
            var result = _participantValidator.Validate(null);

            Assert.Greater(result.Errors.Count(e => e.Contains("null")), 0);
            Assert.AreEqual(result.IsValid, false);
        }

        [Test]
        public void ParticipantValidatorReturnsInvalidIfParticipantNameIsNullOrEmpty()
        {
            var participant = new Participant
            {
                Name = "",
                IpAddress = "1.2.3.4"
            };

            var result = _participantValidator.Validate(participant);

            Assert.Greater(result.Errors.Count(e => e.Contains("Name")), 0);
            Assert.False(result.IsValid);
        }

        [Test]
        public void ParticipantValidatorReturnsInvalidIfParticipantIpAddressIsNullOrEmpty()
        {
            var participant = new Participant
            {
                Name = "Test",
                IpAddress = ""
            };

            var result = _participantValidator.Validate(participant);

            Assert.Greater(result.Errors.Count(e => e.Contains("IpAddress")), 0);
            Assert.False(result.IsValid);
        }
    }
}

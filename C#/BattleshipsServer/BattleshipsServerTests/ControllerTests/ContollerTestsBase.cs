using BattleshipsServer.Interfaces;
using BattleshipsServer.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace BattleshipsServerTests.ControllerTests
{
    public class ControllerTestsBase
    {
        protected Mock<IProcessor<Participant>> MockParticipantProcessor;
        protected Mock<IValidator> MockValidator;

        protected void SetupParticipantValidatorErrors(string[] errors)
        {
            MockValidator
                .Setup(x => x.Validate(It.IsAny<Participant>()))
                .Returns(new ValidatorResult
                {
                    Errors = new List<string>(errors)
                });
        }

        protected static void AssertHttpCode(IActionResult result, int statusCode)
        {
            switch (result.GetType().ToString())
            {
                case "ObjectResult":
                    Assert.That(((ObjectResult)result).StatusCode == statusCode);
                    break;
                
                case "StatusCodeResult":
                    Assert.That(((StatusCodeResult)result).StatusCode == statusCode);
                    break;
            }
        }
    }
}

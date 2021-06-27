using BattleshipsServer.Interfaces;
using BattleshipsServer.Models;

namespace BattleshipsServer.Validators
{
    public class GameParticipantValidator : IValidator<GameParticipant>
    {
        public ValidatorResult Validate(GameParticipant value)
        {
            var validatorResult = new ValidatorResult();

            if (value == null)
            {
                validatorResult.Errors.Add("Request is null");
            }

            if (string.IsNullOrWhiteSpace(value?.Name))
            {
                validatorResult.Errors.Add($"{nameof(value.Name)} is null or whitespace");
            }

            return validatorResult;
        }
    }
}

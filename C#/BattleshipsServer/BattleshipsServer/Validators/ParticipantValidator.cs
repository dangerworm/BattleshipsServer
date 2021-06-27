using BattleshipsServer.Interfaces;
using BattleshipsServer.Models;

namespace BattleshipsServer.Validators
{
    public class ParticipantValidator : IValidator<Participant>
    {
        public ValidatorResult Validate(Participant value)
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

            if (string.IsNullOrWhiteSpace(value?.IpAddress))
            {
                validatorResult.Errors.Add($"{nameof(value.IpAddress)} is null or whitespace");
            }

            return validatorResult;
        }
    }
}

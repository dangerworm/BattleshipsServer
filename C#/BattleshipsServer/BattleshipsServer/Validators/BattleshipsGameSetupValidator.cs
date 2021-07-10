using BattleshipsServer.Interfaces;
using BattleshipsServer.Models;

namespace BattleshipsServer.Validators
{
    public class BattleshipsGameSetupValidator : IValidator<BattleshipsGameSetup>
    {
        public ValidatorResult Validate(BattleshipsGameSetup value)
        {
            var validatorResult = new ValidatorResult();

            if (value == null)
            {
                validatorResult.Errors.Add("Request is null");
            }

            return validatorResult;
        }
    }
}

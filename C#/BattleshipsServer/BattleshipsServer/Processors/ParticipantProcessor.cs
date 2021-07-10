using BattleshipsServer.Enums;
using BattleshipsServer.Helpers;
using BattleshipsServer.Interfaces;
using BattleshipsServer.Models;
using System;
using System.Threading.Tasks;

namespace BattleshipsServer.Processors
{
    public class ParticipantProcessor : IProcessor<Participant>
    {
        private readonly IDataProvider<Participant> _participantDataStore;
        private readonly IValidator _validator;

        public ParticipantProcessor(
            IDataProvider<Participant> participantDataStore,
            IValidator validator
        )
        {
            Verify.NotNull(participantDataStore, nameof(participantDataStore));
            Verify.NotNull(validator, nameof(validator));

            _participantDataStore = participantDataStore;
            _validator = validator;
        }

        public async Task<ProcessorResult> Process(Participant participant, ProcessorOperation operation)
        {
            var result = new ProcessorResult
            {
                ValidatorResult = _validator.Validate(participant)
            };

            if (!result.ValidatorResult.IsValid)
            {
                result.Message = "The registration failed validation. Please review the errors and try again.";
                result.IsSuccess = false;
                return result;
            }

            if (operation == ProcessorOperation.Add)
            {
                try
                {
                    await _participantDataStore.AddItem(participant);
                    result.IsSuccess = true;
                }
                catch (Exception exception)
                {
                    result.Message = exception.Message + ": That name has already been registered. If you are trying to " +
                                     "edit your registration, please use the EditRegistration endpoint.";
                    result.IsSuccess = false;
                }
            }

            if (operation == ProcessorOperation.Edit)
            {
                try
                {
                    await _participantDataStore.EditItem(participant);
                    result.IsSuccess = true;
                }
                catch (Exception exception)
                {
                    result.Message = exception.Message + ": If you are trying to register, please use the Register endpoint.";
                    result.IsSuccess = false;
                }
            }

            return result;
        }
    }
}

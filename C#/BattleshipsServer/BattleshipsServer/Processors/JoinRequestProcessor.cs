using BattleshipsServer.Helpers;
using BattleshipsServer.Interfaces;
using BattleshipsServer.Models;

namespace BattleshipsServer.Processors
{
    public class GameParticipantProcessor : IProcessor<Participant>
    {
        private readonly IDataProvider<Participant> _gameParticipantDataStore;

        public GameParticipantProcessor(IDataProvider<Participant> gameParticipantDataStore)
        {
            Verify.NotNull(gameParticipantDataStore, nameof(gameParticipantDataStore));

            _gameParticipantDataStore = gameParticipantDataStore;
        }

        public void Process(Participant participant)
        {
            _gameParticipantDataStore.AddItem(participant);
        }
    }
}

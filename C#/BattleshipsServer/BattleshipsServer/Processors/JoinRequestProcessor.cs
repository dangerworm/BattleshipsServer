using BattleshipsServer.Helpers;
using BattleshipsServer.Interfaces;
using BattleshipsServer.Models;

namespace BattleshipsServer.Processors
{
    public class GameParticipantProcessor : IProcessor<GameParticipant>
    {
        private readonly IDataProvider<GameParticipant> _gameParticipantDataStore;

        public GameParticipantProcessor(IDataProvider<GameParticipant> gameParticipantDataStore)
        {
            Verify.NotNull(gameParticipantDataStore, nameof(gameParticipantDataStore));

            _gameParticipantDataStore = gameParticipantDataStore;
        }

        public void Process(GameParticipant request)
        {
            // TODO: Add IP Address
            var gameParticipant = new GameParticipant
            {
                Name = request.Name,
                //IpAddress = 
            };

            _gameParticipantDataStore.AddItem(gameParticipant);
        }
    }
}

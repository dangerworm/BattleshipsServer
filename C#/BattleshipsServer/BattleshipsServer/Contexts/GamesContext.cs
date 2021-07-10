using BattleshipsServer.Helpers;
using BattleshipsServer.Interfaces;
using BattleshipsServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleshipsServer.Contexts
{
    public class GamesContext : IGamesContext
    {
        private readonly IDataProvider<GameSettings> _gameSettingsDataProvider;

        private readonly UniqueIdProvider _gameIdProvider;

        private readonly IDictionary<Guid, GameSettings> _gameSettings;

        public GamesContext(
            IDataProvider<GameSettings> gameSettingsDataProvider,
            UniqueIdProvider gameIdProvider)
        {
            Verify.NotNull(gameSettingsDataProvider, nameof(gameSettingsDataProvider));
            Verify.NotNull(gameIdProvider, nameof(gameIdProvider));

            _gameSettingsDataProvider = gameSettingsDataProvider;
            _gameIdProvider = gameIdProvider;

            _gameSettings = new Dictionary<Guid, GameSettings>();
        }

        public async Task<GameSettings> CreateNew(IEnumerable<Participant> participants)
        {
            var newGameId = _gameIdProvider();

            var newGame = new GameSettings
            {
                GameId = newGameId,
                ParticipantNames = participants.Select(p => p.Name),
                Game = "battleships",
                Variation = "",
                GameBoardSettings = new GameBoardSettings
                {
                    Width = 10,
                    Height = 10
                }
            };

            await _gameSettingsDataProvider.AddOrEditItem(newGame);
            _gameSettings.Add(newGameId, newGame);

            return newGame;
        }

        public GameSettings GetGameSettings(Guid gameId)
        {
            if (!_gameSettings.ContainsKey(gameId))
            {
                throw new KeyNotFoundException("The requested game ID does not exist");
            }

            return _gameSettings[gameId];
        }

        public bool IsGameInProgress(Guid gameId)
        {
            return _gameSettings.ContainsKey(gameId);
        }

        public async Task EndGame(Guid gameId)
        {
            await _gameSettingsDataProvider.RemoveItem(_gameSettings[gameId]);

            _gameSettings.Remove(gameId);
        }
    }
}

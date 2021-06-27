using BattleshipsServer.Helpers;
using BattleshipsServer.Interfaces;
using BattleshipsServer.Models;
using System;
using System.Threading.Tasks;

namespace BattleshipsServer
{
    public class GameContext : IGameContext
    {
        private readonly IDataProvider<GameSettings> _gameSettingsDataProvider;

        private readonly UniqueIdProvider _gameIdProvider;

        private GameSettings _gameSettings;

        public GameContext(
            IDataProvider<GameSettings> gameSettingsDataProvider,
            UniqueIdProvider gameIdProvider)
        {
            Verify.NotNull(gameSettingsDataProvider, nameof(gameSettingsDataProvider));
            Verify.NotNull(gameIdProvider, nameof(gameIdProvider));

            _gameSettingsDataProvider = gameSettingsDataProvider;
            _gameIdProvider = gameIdProvider;
        }

        public async Task<GameSettings> BeginGame()
        {
            if (_gameSettings != null)
            {
                throw new InvalidOperationException("A game is already in progress");
            }

            _gameSettings = new GameSettings
            {
                GameId = _gameIdProvider(),
                Game = "battleships",
                Variation = "",
                GameBoardSettings = new GameBoardSettings
                {
                    Width = 10,
                    Height = 10
                }
            };

            await _gameSettingsDataProvider.AddItem(_gameSettings);

            return _gameSettings;
        }

        public bool IsGameInProgress()
        {
            return _gameSettings != null;
        }

        public GameSettings GetGameSettings()
        {
            return _gameSettings;
        }

        public async Task EndGame()
        {
            await _gameSettingsDataProvider.RemoveItem(_gameSettings);

            _gameSettings = null;
        }
    }
}

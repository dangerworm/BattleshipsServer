using BattleshipsServer.Interfaces;
using BattleshipsServer.Models;
using System.IO;

namespace BattleshipsServer.Data
{
    public class GameSettingsDataProvider : FileBasedDataProvider<GameSettings>
    {
        protected sealed override string FilePath => Path.Combine("battleships", FileName);

        private static string FileName => "game-settings.json";

        public GameSettingsDataProvider(IFileDataStore fileDataStore) 
            : base(fileDataStore)
        {
        }

        protected override bool IsMatch(GameSettings existingItem, GameSettings newItem)
        {
            return existingItem.GameId == newItem.GameId;
        }
    }
}

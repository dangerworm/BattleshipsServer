using BattleshipsServer.Interfaces;
using BattleshipsServer.Models;
using System.IO;
using System.Linq;

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
            return
                newItem.ParticipantNames.All(participantName => 
                    existingItem.ParticipantNames.Contains(participantName))
                &&
                existingItem.ParticipantNames.All(participantName =>
                    newItem.ParticipantNames.Contains(participantName));
        }

        protected override void EditItem(GameSettings existingItem, GameSettings newItem)
        {
            existingItem.ParticipantNames = newItem.ParticipantNames;
        }
    }
}

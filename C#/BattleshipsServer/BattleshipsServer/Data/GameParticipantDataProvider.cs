using System;
using System.IO;
using BattleshipsServer.Interfaces;
using BattleshipsServer.Models;

namespace BattleshipsServer.Data
{
    public class GameParticipantDataProvider : FileBasedDataProvider<GameParticipant>
    {
        protected sealed override string FilePath => Path.Combine("battleships", FileName);

        private static string FileName => "game-participants.json";

        public GameParticipantDataProvider(IFileDataStore fileDataStore) 
            : base(fileDataStore)
        {
        }

        protected override bool IsMatch(GameParticipant existingItem, GameParticipant newItem)
        {
            return existingItem.Name == newItem.Name && existingItem.IpAddress == newItem.IpAddress;
        }
    }
}

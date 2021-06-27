using BattleshipsServer.Interfaces;
using BattleshipsServer.Models;
using System.IO;

namespace BattleshipsServer.Data
{
    public class ParticipantDataProvider : FileBasedDataProvider<Participant>
    {
        protected sealed override string FilePath => Path.Combine("battleships", FileName);

        private static string FileName => "participants.json";

        public ParticipantDataProvider(IFileDataStore fileDataStore) 
            : base(fileDataStore)
        {
        }

        protected override bool IsMatch(Participant existingItem, Participant newItem)
        {
            return existingItem.Name == newItem.Name && existingItem.IpAddress == newItem.IpAddress;
        }
    }
}

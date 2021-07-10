using System.Threading.Tasks;
using BattleshipsServer.Enums;
using BattleshipsServer.Models;
using BattleshipsServer.Processors;

namespace BattleshipsServer.Interfaces
{
    public interface ITournamentContext
    {
        public TournamentSettings CreateNew();

        public TournamentSettings GetTournamentSettings();

        public Task<ProcessorResult> ProcessParticipant(Participant participant, ProcessorOperation operation);
        
        public Task Begin();
        public void End();
    }
}

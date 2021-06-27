using BattleshipsServer.Models;

namespace BattleshipsServer.Interfaces
{
    public interface ITournamentContext
    {
        public TournamentSettings CreateNew();

        public TournamentSettings GetTournamentSettings();

        public void End();
    }
}

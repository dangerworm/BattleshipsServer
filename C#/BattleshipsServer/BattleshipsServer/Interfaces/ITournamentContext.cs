using BattleshipsServer.Models;

namespace BattleshipsServer.Interfaces
{
    public interface ITournamentContext
    {
        public void Begin();

        public TournamentSettings GetTournamentSettings();

        public void End();
    }
}

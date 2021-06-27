using System.Threading.Tasks;
using BattleshipsServer.Models;

namespace BattleshipsServer.Interfaces
{
    public interface IGameContext
    {
        Task<GameSettings> BeginGame();
        bool IsGameInProgress();
        GameSettings GetGameSettings();
        Task EndGame();
    }
}

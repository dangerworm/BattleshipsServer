using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BattleshipsServer.Models;

namespace BattleshipsServer.Interfaces
{
    public interface IGameContext
    {
        Task<GameSettings> StartNewGame(IEnumerable<Guid> participantIds);
        
        GameSettings GetGameSettings(Guid gameId);
        
        bool IsGameInProgress(Guid gameId);
        
        Task EndGame(Guid gameId);
    }
}

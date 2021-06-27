using System;
using System.Collections.Generic;

namespace BattleshipsServer.Models
{
    public class GameSettings : TournamentSettings
    {
        public Guid GameId { get; set; }
        
        public IEnumerable<Guid> ParticipantIds { get; set; }
    }
}

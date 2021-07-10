using System;
using System.Collections.Generic;

namespace BattleshipsServer.Models
{
    public class GameSettings : TournamentSettings
    {
        public Guid GameId { get; set; }
        
        public IEnumerable<string> ParticipantNames { get; set; }
    }
}

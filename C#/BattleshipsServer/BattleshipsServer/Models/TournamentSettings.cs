using System;

namespace BattleshipsServer.Models
{
    public class TournamentSettings
    {
        public string Game { get; set; }
        public string Variation { get; set; }
        public GameBoardSettings GameBoardSettings { get; set; }
    }
}

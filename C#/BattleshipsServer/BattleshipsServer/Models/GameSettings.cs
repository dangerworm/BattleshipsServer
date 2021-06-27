using System;

namespace BattleshipsServer.Models
{
    public class GameSettings
    {
        public Guid GameId { get; set; }
        public string Game { get; set; }
        public string Variation { get; set; }
        public GameBoardSettings GameBoardSettings { get; set; }
    }
}

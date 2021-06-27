﻿using BattleshipsServer.Interfaces;
using BattleshipsServer.Models;

namespace BattleshipsServer.Contexts
{
    public class TournamentContext : ITournamentContext
    {
        private TournamentSettings _tournamentSettings;

        public void Begin()
        {
            _tournamentSettings = new TournamentSettings
            {
                Game = "battleships",
                Variation = "",
                GameBoardSettings = new GameBoardSettings
                {
                    Width = 10,
                    Height = 10
                }
            };
        }

        public TournamentSettings GetTournamentSettings()
        {
            return _tournamentSettings;
        }

        public void End()
        {
            _tournamentSettings = null;
        }
    }
}

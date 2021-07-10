using System;
using BattleshipsServer.Enums;
using BattleshipsServer.Helpers;
using BattleshipsServer.Interfaces;
using BattleshipsServer.Models;
using BattleshipsServer.Processors;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace BattleshipsServer.Contexts
{
    public class TournamentContext : ITournamentContext
    {
        private readonly IDataProvider<Participant> _participantDataStore;
        private readonly IGamesContext _gamesContext;
        private readonly IProcessor<Participant> _participantProcessor;

        private TournamentSettings _tournamentSettings;
        private ICollection<GameSettings> _games;

        public TournamentContext(
            IDataProvider<Participant> participantDataStore,
            IGamesContext gamesContext,
            IProcessor<Participant> participantProcessor
        )
        {
            Verify.NotNull(participantDataStore, nameof(participantDataStore));
            Verify.NotNull(gamesContext, nameof(gamesContext));
            Verify.NotNull(participantProcessor, nameof(participantProcessor));

            _participantDataStore = participantDataStore;
            _gamesContext = gamesContext;
            _participantProcessor = participantProcessor;

            _games = new Collection<GameSettings>();
        }

        public TournamentSettings CreateNew()
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

            return _tournamentSettings;
        }

        public TournamentSettings GetTournamentSettings()
        {
            return _tournamentSettings;
        }

        public Task<ProcessorResult> ProcessParticipant(Participant participant, ProcessorOperation operation)
        {
            return _participantProcessor.Process(participant, operation);
        }

        public Task Begin()
        {
            if (_tournamentSettings.InProgress)
            {
                throw new InvalidOperationException("Tournament is already in progress.");
            }

            _tournamentSettings.InProgress = true;
            return CreateGames();
        }

        public void End()
        {
            if (!_tournamentSettings.InProgress)
            {
                throw new InvalidOperationException("Tournament is not in progress.");
            }

            _tournamentSettings.InProgress = false;
            _games = new Collection<GameSettings>();
            _tournamentSettings = null;
        }

        private async Task CreateGames()
        {
            var participants = (await _participantDataStore.GetItems()).ToArray();

            for (var participantAIndex = 0; participantAIndex < participants.Length - 1; participantAIndex++)
            {
                for (var participantBIndex = participantAIndex + 1; participantBIndex < participants.Length; participantBIndex++)
                {
                    var participantA = participants[participantAIndex];
                    var participantB = participants[participantBIndex];

                    if (participantA.Name == participantB.Name)
                    {
                        continue;
                    }

                    var game = await _gamesContext.CreateNew(new[] { participantA, participantB });
                    _games.Add(game);
                }
            }
        }
    }
}

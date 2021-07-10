using System;
using System.Collections.Generic;
using System.Text;
using BattleshipsServer.Contexts;
using BattleshipsServer.Controllers;
using BattleshipsServer.Interfaces;
using BattleshipsServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace BattleshipsServerTests.ContextTests
{

    public class TournamentContextTests
    {
        private Mock<ITournamentContext> _mockTournamentContext;
        
        [SetUp]
        public void Setup()
        {
            _mockTournamentContext = new Mock<ITournamentContext>();
        }

        [Test]
        public void CreateNewCallsCreateNewOnTournamentContext()
        {
         
        }
    }
}

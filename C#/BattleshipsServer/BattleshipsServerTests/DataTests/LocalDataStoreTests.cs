using System.Collections.Generic;
using BattleshipsServer.Data;
using BattleshipsServer.Interfaces;
using BattleshipsServer.Models;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace BattleshipsServerTests.DataTests
{
    public class LocalDataStoreTests
    {
        private Mock<IOptions<FileStoreOptions>> _mockFileStoreOptions;
        private Mock<IFileDataStore> _mockLocalDataStore;

        [SetUp]
        public void Setup()
        {
            _mockFileStoreOptions = new Mock<IOptions<FileStoreOptions>>();

            _mockFileStoreOptions
                .Setup(x => x.Value)
                .Returns(new FileStoreOptions
                {
                    Path = "test-path"
                });

            _mockLocalDataStore = new Mock<IFileDataStore>();
        }

        [Test]
        public async Task LoadUsesCorrectPath()
        {
            _mockLocalDataStore.Setup(x => x.Load(It.IsAny<string>())).ReturnsAsync("");
            var classUnderTest = new GameParticipantDataProvider(_mockLocalDataStore.Object);
            var expectedPath = Path.Combine("battleships", "game-participants.json");

            await classUnderTest.AddItem(new GameParticipant());

            _mockLocalDataStore.Verify(x => x.Load(It.Is<string>(s => s == expectedPath)), Times.Once());
        }

        [Test]
        public async Task AddItemAddsItem()
        {
            var classUnderTest = new GameParticipantDataProvider(_mockLocalDataStore.Object);
            var gameParticipants = new[]
            {
                new GameParticipant
                {
                    Name = "TestName",
                    IpAddress = "1.2.3.4"
                }
            };
            
            var gameParticipantsJson = JsonSerializer.Serialize(gameParticipants, 
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            await classUnderTest.AddItem(gameParticipants[0]);

            _mockLocalDataStore.Verify(x => x.Save(It.IsAny<string>(), It.Is<string>(s => s == gameParticipantsJson)), Times.Once());
        }

        [Test]
        public async Task RemoveItemRemovesItem()
        {
            var classUnderTest = new GameParticipantDataProvider(_mockLocalDataStore.Object);
            var gameParticipants = new[]
            {
                new GameParticipant
                {
                    Name = "TestName",
                    IpAddress = "1.2.3.4"
                }
            };
            var gameParticipantsJson = JsonSerializer.Serialize(gameParticipants,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            _mockLocalDataStore.Setup(x => x.Load(It.IsAny<string>())).ReturnsAsync(gameParticipantsJson);

            await classUnderTest.RemoveItem(gameParticipants[0]);

            _mockLocalDataStore.Verify(x => x.Save(It.IsAny<string>(), It.Is<string>(s => s == "[]")), Times.Once());
        }

        [Test]
        public async Task SaveUsesCorrectPath()
        {
            var classUnderTest = new GameParticipantDataProvider(_mockLocalDataStore.Object);
            var expectedPath = Path.Combine("battleships", "game-participants.json");

            await classUnderTest.AddItem(new GameParticipant());

            _mockLocalDataStore.Verify(x => x.Save(It.Is<string>(s => s == expectedPath), It.IsAny<string>()), Times.Once());
        }
    }
}

﻿using BattleshipsServer.Helpers;
using BattleshipsServer.Models;
using NUnit.Framework;
using System.Text.Json;

namespace BattleshipsServerTests.HelperTests
{
    public class JsonListHelperTests
    {
        [Test]
        public void AddItemAddsItem()
        {
            var gameParticipants = new[]
            {
                new GameParticipant
                {
                    Name = "TestName",
                    IpAddress = "1.2.3.4"
                }
            };

            var expectedOutput = JsonSerializer.Serialize(
                gameParticipants,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            var result = JsonListHelper.AddItem("[]", item => true, gameParticipants[0]);

            Assert.AreEqual(result, expectedOutput);
        }

        [Test]
        public void RemoveItemRemovesItems()
        {
            var gameParticipants = new[]
            {
                new GameParticipant
                {
                    Name = "TestName",
                    IpAddress = "1.2.3.4"
                }
            };

            var input = JsonSerializer.Serialize(
                gameParticipants,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            var result = JsonListHelper.RemoveItem<GameParticipant>(input, item => true);

            Assert.AreEqual(result, "[]");
        }
    }
}
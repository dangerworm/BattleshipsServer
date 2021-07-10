using System;

namespace BattleshipsServer.Models
{
    public class Participant
    {
        public string Name { get; set; }
        public string IpAddress { get; set; }
        public int Port { get; set; }
    }
}

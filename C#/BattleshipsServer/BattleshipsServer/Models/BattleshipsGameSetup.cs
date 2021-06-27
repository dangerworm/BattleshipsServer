namespace BattleshipsServer.Models
{
    public class BattleshipsGameSetup
    {
        public ShipPosition Carrier { get; set; }
        public ShipPosition Battleship { get; set; }
        public ShipPosition Cruiser { get; set; }
        public ShipPosition Submarine { get; set; }
        public ShipPosition Destroyer { get; set; }

    }
}

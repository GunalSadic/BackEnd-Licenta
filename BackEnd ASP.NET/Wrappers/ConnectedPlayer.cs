using BackEnd_ASP.NET.Entities;

namespace BackEnd_ASP.NET.Wrappers
{
    public class ConnectedPlayer 
    {
        public string connectionId;
        public Player Player { get; set; }
        public ConnectedPlayer(string connectionId, Player player)
        {
            this.connectionId = connectionId;
            this.Player = player;
            
        }
    }
}

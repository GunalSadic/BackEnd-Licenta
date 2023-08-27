using BackEnd_ASP.NET.DTO;
using BackEnd_ASP.NET.Entities;
using BackEnd_ASP.NET.Migrations;
using BackEnd_ASP.NET.Wrappers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Collections;
using System.Numerics;
using System.Text.RegularExpressions;

namespace BackEnd_ASP.NET
{
    [Authorize (AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GameHub : Hub
    {
        private readonly ApplicationDbContext _context;
        public static List<ConnectedPlayer> _Players = new List<ConnectedPlayer>();
        public static List<Tuple<ConnectedPlayer,ConnectedPlayer>> _MatchedPlayers = new List<Tuple<ConnectedPlayer,ConnectedPlayer>>();    
        private static readonly object _lock = new object();
        public GameHub(ApplicationDbContext context)
        {
            this._context = context;
        }

        #region Overrides
      

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            _Players.Remove(_Players.Where(x => x.connectionId == Context.ConnectionId).FirstOrDefault());
            return base.OnDisconnectedAsync(exception);
        }
        #endregion

        #region Methods
        public async Task SendMessage(string user, string message)
        {   
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
        #region Queue related logic
        public async Task JoinQueue(string userEmail)
        {
            ConnectedPlayer player1 =  new ConnectedPlayer( Context.ConnectionId,_context.Players.Where(x => x.Email == userEmail).FirstOrDefault());
            if(player1 != null && !_Players.Contains(player1))
                _Players.Add(player1);
            lock( _lock )
            {
                if (!_MatchedPlayers.Any(x => x.Item1.connectionId == Context.ConnectionId || x.Item2.connectionId == Context.ConnectionId))
                {
                    ConnectedPlayer player2 = findOpponent(player1);
                    if(player2 != null)
                    {
                        // I should probaly remove the players from the list of Players once they become matched
                        // And also rename the list to PlayerPool or AvailablePlayers
                        Tuple<ConnectedPlayer, ConnectedPlayer> match = Tuple.Create(player1, player2);
                        _MatchedPlayers.Add(match);
                        gameFound(match);
                    }
                }
            }

        }

        public ConnectedPlayer findOpponent(ConnectedPlayer player)
        {
            return _Players.Where(x => x.Player.Elo >= player.Player.Elo - 50 && x.Player.Elo <= player.Player.Elo + 50 
                                    && x.connectionId != player.connectionId).FirstOrDefault();
            
        }
        public async void gameFound(Tuple<ConnectedPlayer, ConnectedPlayer> match)
        {
            string response = JsonConvert.SerializeObject(new
            {
                player1Email = match.Item1.Player.Email,
                player2Email = match.Item2.Player.Email,
                player1Color = "white",
                player2Color = "black"
            });
            await Clients.Client(match.Item1.connectionId).SendAsync("MatchFound", response);
            await Clients.Client(match.Item2.connectionId).SendAsync("MatchFound", response);
        }

        public async Task MakeMove(string json)
        {
            MoveDTO moveObj = JsonConvert.DeserializeObject<MoveDTO>(json);
            string opponentConnectionId = _Players.Where(x => x.Player.Email == moveObj.opponentEmail).Select(x => x.connectionId).FirstOrDefault();
            await Clients.Client(opponentConnectionId).SendAsync("ReceiveMove", moveObj.origin, moveObj.dest);
        }
        #endregion
        #endregion
    }
}

using BackEnd_ASP.NET.Entities;
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
    public class GameHub : Hub
    {
        private readonly ApplicationDbContext _context;
        public static Dictionary<int, String> _PlayerConnectionId = new Dictionary<int, string>();
        public static List<Player> _Players = new List<Player>();
        public static List<Tuple<ConnectedPlayer, ConnectedPlayer>> MatchedPlayers= new List<Tuple<ConnectedPlayer, ConnectedPlayer>>();
        public GameHub(ApplicationDbContext context)
        {
            this._context = context;
        }

        #region Overrides
      

        public override Task OnDisconnectedAsync(Exception? exception)
        {
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
            Player player1 = _context.Players.Where(x => x.Email == userEmail).FirstOrDefault();
            Player player2 = null;  
            if (player1 != null)
            {
                if (!_Players.Contains(player1) && !_PlayerConnectionId.ContainsKey(player1.PlayerId))
                {
                    _Players.Add(player1);
                    _PlayerConnectionId.Add(player1.PlayerId, Context.ConnectionId);
                }
                
            }
            else
                return;

            await Clients.All.SendAsync("QueuePlayerCountUpdate", _Players.Count);

            bool foundMatch = false;
            int eloRange = 50;
            player2 = FindOpponent(player1);
            string player1ConnectionId = _PlayerConnectionId[player1.PlayerId];
            string player2ConnectionId = _PlayerConnectionId[player2.PlayerId];
            await Groups.AddToGroupAsync(player2ConnectionId, player1.Email + " " + player2.Email);
            await Groups.AddToGroupAsync(player1ConnectionId, player1.Email + " " + player2.Email);
            string MatchFoundResponse = JsonConvert.SerializeObject(
              new
              {
                  player1Email = player1.Email,
                  player2Email = player2.Email,
                  player1Color = "white",
                  player2Color = "black"
              });
            await Clients.Group(player1.Email + " " + player2.Email).SendAsync("MatchFound", MatchFoundResponse);
            _Players.Remove(player1);
            _Players.Remove(player2);
            MatchedPlayers.Add(new Tuple<ConnectedPlayer, ConnectedPlayer>(
                new ConnectedPlayer(_PlayerConnectionId[player1.PlayerId] , player1),
                new ConnectedPlayer(_PlayerConnectionId[player2.PlayerId],player2)));

            _PlayerConnectionId.Remove(player1.PlayerId);
            _PlayerConnectionId.Remove(player2.PlayerId);
            Console.WriteLine(MatchedPlayers);
        }

        public Player FindOpponent(Player player1 )
        {
            Player player2 = null; 
            bool foundMatch = false;
            int eloRange = 50;
            while (!foundMatch)
            {
                player2 = _Players.Where(x => player1.Elo - eloRange <= x.Elo && x.Elo <= player1.Elo + eloRange
                && x.PlayerId != player1.PlayerId
                ).FirstOrDefault();
                if (player2 == null)
                {
                    Thread.Sleep(5000);
                    eloRange += 50;
                }
                else
                {
                    return player2;
                   
                }
            }
            return player2;
        }

        #endregion

        public async Task MakeMove(string userEmail, string opponentEmail, string move)
        {
            var GameLobby = Clients.Group(userEmail + " " + opponentEmail);
            Tuple<ConnectedPlayer,ConnectedPlayer> players = MatchedPlayers.Where(x => x.Item1.Player.Email == userEmail || x.Item2.Player.Email == userEmail).FirstOrDefault();
            ConnectedPlayer opponent = players.Item1.Player.Email == opponentEmail ? players.Item1 : players.Item2;
            await Clients.Client(opponent.connectionId).SendAsync("ReceiveMove", move);
        }

        #endregion
    }
}

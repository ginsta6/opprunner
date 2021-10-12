using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Runner.SignalR.Hubs
{
    public class RunnerHub : Hub
    {
        public int minPlayers = 2;
        public static int currPlayers = 0;
        private static string players = "";

        public async Task SendTauntMessage(string message)
        {
            Console.WriteLine(currPlayers+" "+players);
            currPlayers++;
            players +=message+'\n';
            await Clients.All.SendAsync("ReceiveTauntMessage", players);
            await Clients.All.SendAsync("ReceivePlayerCount", currPlayers);
        }

        public async Task SendStartSignal()
        {
            await Clients.All.SendAsync("ReceiveStartSignal");
        }
        //public Task JoinGroup(string group)
        //{
        //    return Groups.AddToGroupAsync(Context.ConnectionId, group);
        //}
        //public Task SendMessageToGroup(string group, string message)
        //{
        //    return Clients.Group(group).SendAsync("ReceiveMessage", message);
        //}

    } 
}

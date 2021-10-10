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

        public async Task SendTauntMessage(string message)
        {
            Console.WriteLine(currPlayers);
            currPlayers++;
            await Clients.All.SendAsync("ReceiveTauntMessage", message);
            await Clients.All.SendAsync("ReceivePlayerCount", currPlayers);
        }

        public async Task SendStartSignal()
        {
            await Clients.All.SendAsync("ReceiveStartSignal");
        }

    } 
}

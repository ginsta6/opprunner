using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Runner.SignalR.Hubs
{
    public class RunnerHub : Hub
    {
        //public int minPlayers = 2;
        //public static int currPlayers = 0;
        //private static string players = "";
        //private static List<string> playerTypes = new List<string>();

        private SharedRecourses instance = SharedRecourses.getInstance();
        
        public async Task SendTauntMessage(string message, string type)
        {
            Console.WriteLine(instance.currPlayers+" "+instance.players);
            instance.currPlayers++;
            instance.players +=message+'\n';
            instance.playerTypes.Add(type);
            Console.WriteLine("Connected player: " + message + " type: " + type);
            await Clients.All.SendAsync("ReceiveTauntMessage", instance.players);
            await Clients.All.SendAsync("ReceivePlayerCount", instance.currPlayers);
            await Clients.All.SendAsync("ReceivePlayerType", instance.playerTypes);
        }

        public async Task SendStartSignal()
        {
            Console.WriteLine("Starting game");
            await Clients.All.SendAsync("ReceiveStartSignal");
        }

        public async Task SendPlayerState(int type)
        {
            await Clients.Others.SendAsync("ReceivePlayerState", type);
        }

        public async Task SendPlayerJump(bool jumping)
        {
            await Clients.Others.SendAsync("ReceivePlayerJump", jumping);
        }
        public async Task SendUndoSignal()
        {
            await Clients.Others.SendAsync("ReceiveUndoSignal");
        }

        public async Task SendChangeLevelSignal()
        {
            await Clients.All.SendAsync("ReceiveChangeLevelSignal");
        }

        public async Task SendEndGameSignal()
        {
            await Clients.All.SendAsync("ReceiveEndGameSignal");
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

    public class SharedRecourses
    {
        public int minPlayers = 2;
        public int currPlayers = 0;
        public string players = "";
        public List<string> playerTypes = new List<string>();

        private static readonly object _lock = new object();
        private static SharedRecourses instance = null;

        private SharedRecourses()
        {
            Console.WriteLine("Singleton initialiazed");
        }

        public static SharedRecourses getInstance()
        {
            lock(_lock)
                {
                if(instance == null)
                {
                    instance = new SharedRecourses();
                }
            }
            return instance;
        }

        public static SharedRecourses Instance
        {
            get
            {
                lock(_lock)
                {
                    if (instance == null)
                    {
                        instance = new SharedRecourses();
                    }
                    return instance;
                }
            }
        }
        
    }
}

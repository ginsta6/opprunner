using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Runner.SignalR.Hubs
{
    public class RunnerHub : Hub
    {
        
        private SharedRecourses instance = SharedRecourses.getInstance();
        private Interpreter interp;

        public RunnerHub() : base()
        {
            interp = new Interpreter(this);
        }

        
        
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

        public async Task SendAddPointsSignal(int number)
        {
            Console.WriteLine("adding points");
            await Clients.All.SendAsync("ReceiveAddPointsSignal", number);
        }
        public async Task SendRemovePointsSignal(int number)
        {
            await Clients.All.SendAsync("ReceiveRemovePointsSignal", number);
        }

        public async void AllowConsoleCommand()
        {
            Console.WriteLine("Write Your Command \n");
            var test = Console.ReadLine();
            interp.interpret(test);
        }
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

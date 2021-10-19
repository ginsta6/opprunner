using Microsoft.AspNetCore.SignalR.Client;
using Runner2.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner2.Services
{
    public class SignalRService : Subject
    {
        private readonly HubConnection _connection;
        
        public SignalRService(HubConnection connection)
        {
            _connection = connection;

            _connection.On<string>("ReceivePlayerMessage", (message) => Notify(message));
            _connection.On<int>("ReceivePlayerCount", (currPlayer) => Notify(currPlayer));
            _connection.On("ReceiveStartSignal", () => Notify("StartSignal"));
        }

        public async Task Connect()
        {
            await _connection.StartAsync();
        }

        public async Task SendPlayerMessage(string message)
        {
            await _connection.SendAsync("SendPlayerMessage", message);
        }
        
        public async Task SendSignalToServer(string methodName)
        {
            await _connection.SendAsync(methodName);
        }
    }
}

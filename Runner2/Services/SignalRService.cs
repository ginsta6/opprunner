using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner2.Services
{
    public class SignalRService
    {
        private readonly HubConnection _connection;

        public event Action<string> TauntMessageReceived;
        public event Action<List<string>> PlayerTypeReceived;
        public event Action<int> PlayerCountReceived;
        public event Action StartSignalReceived;

        public SignalRService(HubConnection connection)
        {
            _connection = connection;

            _connection.On<string>("ReceiveTauntMessage", (message) => TauntMessageReceived?.Invoke(message));
            _connection.On<List<string>>("ReceivePlayerType", (message) => PlayerTypeReceived?.Invoke(message));
            _connection.On<int>("ReceivePlayerCount", (currPlayer) => PlayerCountReceived?.Invoke(currPlayer));
            _connection.On("ReceiveStartSignal", () => StartSignalReceived?.Invoke());
        }

        public async Task Connect()
        {
            await _connection.StartAsync();
        }

        public async Task SendTauntMessage(string message, string playerType)
        {
            await _connection.SendAsync("SendTauntMessage", message, playerType);
        }

        public async Task SendStartSignal()
        {
            await _connection.SendAsync("SendStartSignal");
        }
    }
}

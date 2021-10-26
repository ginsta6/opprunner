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
        public event Action<int> PlayerStateReceived;
        public event Action StartSignalReceived;
        public event Action<bool> PlayerJumpReceived;

        public SignalRService(HubConnection connection)
        {
            _connection = connection;

            _connection.On<string>("ReceiveTauntMessage", (message) => TauntMessageReceived?.Invoke(message));
            _connection.On<List<string>>("ReceivePlayerType", (list) => PlayerTypeReceived?.Invoke(list));
            _connection.On<int>("ReceivePlayerCount", (currPlayer) => PlayerCountReceived?.Invoke(currPlayer));
            _connection.On<int>("ReceivePlayerState", (state) => PlayerStateReceived?.Invoke(state));
            _connection.On("ReceiveStartSignal", () => StartSignalReceived?.Invoke());
            _connection.On<bool>("ReceivePlayerJump", (jumping) => PlayerJumpReceived?.Invoke(jumping));
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

        public async Task SendPlayerState(int state)
        {
            await _connection.SendAsync("SendPlayerState", state);
        }

        public async Task SendPlayerJump(bool jumping)
        {
            await _connection.SendAsync("SendPlayerJump", jumping);
        }
    }
}

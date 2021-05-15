using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace RoboMajkel
{
    public class CommandHandler : IDisposable
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;
        private readonly IServiceProvider _service;
        public CommandHandler(DiscordSocketClient client, CommandService commands, IServiceProvider service)
        {
            _client = client;
            _commands = commands;
            _service = service;
        }

        public void Dispose()
        {
            _client.MessageReceived -= HandleCommandAsync;
        }

        public async Task InstallCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;
            await _commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(),
                                        services: _service);
        }
        private async Task HandleCommandAsync(SocketMessage messageParam)
        {
            
            var message = messageParam as SocketUserMessage;
            if (message == null) return;
            int argPos = 0;
            if (!(message.HasCharPrefix('!', ref argPos) ||
            message.HasMentionPrefix(_client.CurrentUser, ref argPos)) ||
            message.Author.IsBot)
                return;

            var context = new SocketCommandContext(_client, message);

            await _commands.ExecuteAsync(
                context: context,
                argPos: argPos,
                services: _service);

        }
    }
}

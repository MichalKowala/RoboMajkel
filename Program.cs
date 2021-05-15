using Discord.WebSocket;
using System;
using System.Threading.Tasks;
using Discord;
using RoboMajkel.Utilities;
using System.Threading;
using Discord.Commands;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using RoboMajkel.Interfaces;

namespace RoboMajkel
{
    class Program
    {
        private readonly DiscordSocketClient _client;
        private CommandService _commandService;
        private LoggingService _loggingService;
        private CommandHandler _commandHandler;


        public IServiceProvider BuildServiceProvider() => new ServiceCollection()
            .AddMemoryCache()
            .AddTransient<ICachingService, CachingService>()
            .BuildServiceProvider();

        static void Main(string[] args)
        {

            new Program().MainAsync().GetAwaiter().GetResult();
        }


        public Program()
        {

            _client = new DiscordSocketClient();
            _commandService = new CommandService();
            _loggingService = new LoggingService(_client, _commandService);
            _commandHandler = new CommandHandler(_client, _commandService, BuildServiceProvider());
        }

        public async Task MainAsync()
        {


            await _commandHandler.InstallCommandsAsync();
            await _client.LoginAsync(TokenType.Bot, EnvVars.RoboMajkelToken);
            await _client.StartAsync();

            await Task.Delay(-1);
        }
    }
}

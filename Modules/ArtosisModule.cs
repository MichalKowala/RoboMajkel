using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboMajkel.Modules
{
    public class ArtosisModule : ModuleBase<SocketCommandContext>
    {
        [Command("artoclip")]
        [Summary("Plays random artosis clip")]
        public async Task ArtoClip()
        {
            await Context.Channel.SendMessageAsync("testutestu");
        }
    }
}

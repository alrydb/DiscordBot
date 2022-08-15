using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DiscordBot.Models;
using System.Net;

namespace DiscordBot.Commands
{
    public class FunCommands : BaseCommandModule
    {
      
            [Command("ping")]
        [Description("Returns pong")]
        public async Task Ping(CommandContext ctx)
        {
            // Lägg alltid till ConfigureAwait(false) på alla await anrop
            await ctx.Channel.SendMessageAsync("Pong").ConfigureAwait(false);
        }

        [Command("Sweine")]
        public async Task Add(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("TV").ConfigureAwait(false);
        }



    }
}

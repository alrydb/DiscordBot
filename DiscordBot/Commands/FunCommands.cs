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
using DSharpPlus.Entities;
using DSharpPlus.VoiceNext;
using DSharpPlus;
using DSharpPlus.EventArgs;

namespace DiscordBot.Commands
{
    public class FunCommands : BaseCommandModule
    {
        
      

        [Command("Sweine")]
        public async Task Sweine(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("TV").ConfigureAwait(false);
        }


        [Command("Chipsa")]
        public async Task Chipsa(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("https://tenor.com/view/chipsa-chipsaroy-roy-valorant-sova-gif-23510634").ConfigureAwait(false);
        }

        [Command("Peaceout")]
        public async Task PeaceOut(CommandContext ctx)
        {
            DiscordMember member = ctx.Member;
            //DiscordChannel channel = await ctx.Client.GetChannelAsync(0);
            DiscordMember d;


            //VoiceNextConnection connection;
            //VoiceNextExtension extension;

            //var vnext = ctx.Client.GetVoiceNext();
            //var connection = vnext.GetConnection(ctx.Guild);

            //connection.Disconnect();


            //VoiceNextConnection.GetConnection(ctx.Member.Guild);


            //member.SetMuteAsync(true, null);

            //await ctx.Channel.SendMessageAsync(ev.ToString()).ConfigureAwait(false);

            try
            {

                //await member.TimeoutAsync(null, null)
                //.ConfigureAwait(false);
                //await ctx.Channel.SendMessageAsync(ctx.Guild.Name).ConfigureAwait(false);
                //connection.Dispose();
                //await ctx.Channel.SendMessageAsync(member.ToString()).ConfigureAwait(false);
                //await member.SetMuteAsync(true, "bc");

                //await member.PlaceInAsync(null).ConfigureAwait(false);
                //await member.TimeoutAsync(until: null, reason: null).ConfigureAwait(false);

                //await ctx.Member.SendMessageAsync("test").ConfigureAwait(false);
                
                //await ctx.Member.SetDeafAsync(deaf: true, reason: "test");
                //await ctx.Channel.SendMessageAsync(ctx.Member.Nickname).ConfigureAwait(false);
                //await ctx.Member.TimeoutAsync(until: DateTime.Now.AddSeconds(1), reason: "test").ConfigureAwait(false);
                await ctx.Channel.SendMessageAsync("https://tenor.com/view/peace-disappear-vanish-gif-9727828").ConfigureAwait(false);

                //await ctx.Member.RemoveAsync("test");
            }
            catch(Exception e)
            {
                await ctx.Channel.SendMessageAsync(e.Message).ConfigureAwait(false);
            }
            
           
                //var vnext = ctx.Client.GetVoiceNext();

            //var c = await vnext.ConnectAsync(ctx.Channel);
                //VoiceNextConnection connection = vnext.GetConnection(ctx.Guild);
                //connection.VoiceReceived -= VoiceReceiveHandler;





                //await ctx.Channel.SendMessageAsync("https://tenor.com/view/peace-disappear-vanish-gif-9727828").ConfigureAwait(false);
                //await ctx.Client.DisconnectAsync().ConfigureAwait(false);
                //await connection.Disconnect();
                //connection.Disconnect();
            //    try
            //{
            //    //c.Disconnect();
            //    connection.Disconnect();
            //}
            //catch (Exception e)
            //{
                
            //    await ctx.Channel.SendMessageAsync(ctx.Guild.ToString()).ConfigureAwait(false);
            //    await ctx.Channel.SendMessageAsync(e.Message).ConfigureAwait(false);
            //}
            

          
            




        }
        [Command("afk")]
        public async Task afk(CommandContext ctx)
        {
            DiscordChannel channel = await ctx.Client.GetChannelAsync(1010277521553625158);
            

            await ctx.Member.PlaceInAsync(channel).ConfigureAwait(false);

            //await ctx.Member.SetDeafAsync(deaf: true, null);

        }


    }
}

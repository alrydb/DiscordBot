using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DiscordBot.Models;
using DiscordBot;
using DSharpPlus.Entities;
using System.Net;
using DSharpPlus;

namespace DiscordBot.Commands
{
    [Group("wordGame")]
    public class WordGameCommands : BaseCommandModule
    {
        WordGame wordGame = new WordGame();
        Word word = null;
        


        [Command("word")]
        public async Task Word(CommandContext ctx)

        {
            //wordGame.SetDailyWord();


            word = await WordGame.GetWordAsync(wordGame.DailyWord);

                string message = string.Empty;

                List<DiscordEmoji> emojis = new List<DiscordEmoji>();


                for (int i = 0; i < wordGame.DailyWord.Length; i++)
                {
                    emojis.Add((DiscordEmoji.FromName(ctx.Client, ":blue_square:")));
                }


                foreach (DiscordEmoji emoji in emojis)
                {
                    message += emoji + " ";

                }

                await ctx.Channel.SendMessageAsync(message).ConfigureAwait(false);
                //await ctx.Channel.SendMessageAsync(emojis[1]).ConfigureAwait(false);
           


        }

        [Command("guess")]
        public async Task Guess(CommandContext ctx, string guess)

        { 

            if(guess.Equals(wordGame.DailyWord))
            {
                await ctx.Channel.SendMessageAsync("Du gissade rätt!, wow, grattis!!!!").ConfigureAwait(false);
            }
            else
            {
                await ctx.Channel.SendMessageAsync("Du gissade fel, buuuuh!").ConfigureAwait(false);
            }

            
        }

        [Command("clue1")]
        public async Task Clue1(CommandContext ctx)

        {

            Random random = new Random();

            Char[] dailyWordCharArray = wordGame.DailyWord.ToCharArray();
            var randomChar = dailyWordCharArray[random.Next(dailyWordCharArray.Length + 1)];

                await ctx.Channel.SendMessageAsync("Ledtråd 1, ordet innehåller bokstaven: " + randomChar).ConfigureAwait(false);

        }

        [Command("clue2")]
        public async Task Clue2(CommandContext ctx)

        {
            await ctx.Channel.SendMessageAsync("Ledtråd 2, definiton av ordet: " + word.Definition).ConfigureAwait(false);

        }

        [Command("clue3")]
        public async Task Clue3(CommandContext ctx)

        {
            await ctx.Channel.SendMessageAsync("Ledtråd 3, synonym: " + word.Synonyms[0]).ConfigureAwait(false);

        }








    }

}

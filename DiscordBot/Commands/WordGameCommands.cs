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
    [Group("wg")]
    [Description("Ett spel som generear slumpässiga, svenska, ord som man sedan ska gissa sig fram till.")]
    public class WordGameCommands : BaseCommandModule
    {
        WordGame wordGame = new WordGame();
        Word word = null;
        Char[] dailyWordCharArray;


        private char? clueChar = null;
        private char? startingChar = null;
        private int clueCharIndex;


        bool gameHasStarted = false;
        bool showHiddenChar = false;




        [Command("play")]
        [Description("Startar ett nytt spel och genererar ett nytt ord.")]
        public async Task Play(CommandContext ctx)

        {

            if (!gameHasStarted)
            {
                await wordGame.InitalizeGame();
                word = await WordGame.GetWordAsync(wordGame.DailyWord);

                gameHasStarted = true;
            }
            else
            {
                await ResetGame();
            }



            string message = DisplayHiddenWord(ctx);


            await ctx.Channel.SendMessageAsync("Nytt spel startat " + '\n' + "Ordets längd: " + wordGame.DailyWord.Length + '\n' +
                "Skriv '?help wordgame' för instruktioner och hjälp" + '\n' + message).ConfigureAwait(false);



        }

        [Command("guess")]
        [Description("Används för att gissa ordet. Tar en gissning som input, e.g '?wordgame guess *Katt*'.")]
        public async Task Guess(CommandContext ctx, string guess)

        {
            if (gameHasStarted)
            {
                if (guess.Equals(wordGame.DailyWord))
                {

                    await ctx.Channel.SendMessageAsync("Du gissade rätt! " + DiscordEmoji.FromName(ctx.Client, ":clap:") + DiscordEmoji.FromName(ctx.Client, ":partying_face:") + '\n' + DisplayWord(ctx)).ConfigureAwait(false);
                    await ResetGame();
                }
                else
                {

                    await ctx.Channel.SendMessageAsync("Du gissade fel " + DiscordEmoji.FromName(ctx.Client, ":rofl:") + DiscordEmoji.FromName(ctx.Client, ":rofl:") + DiscordEmoji.FromName(ctx.Client, ":rofl:") +
                        '\n' + DisplayHiddenWord(ctx)).ConfigureAwait(false);
                }
            }
            else
            {
                await ctx.Channel.SendMessageAsync("Du måste starta ett spel för att gissa. Se ?help wordgame").ConfigureAwait(false);
            }




        }

        [Command("clue1")]
        [Description("Avslöjar en slumpmässig bokstav i ordet.")]
        public async Task Clue1(CommandContext ctx)

        {
            if (gameHasStarted && !showHiddenChar)
            {
                try
                {
                    string message = string.Empty;
                   
                    Random random = new Random();
                    Char[] dailyWordCharArray = wordGame.DailyWord.ToCharArray();
                    clueCharIndex = random.Next(dailyWordCharArray.Length);
                    if (clueCharIndex == 0)
                    { clueCharIndex = 1; }
                    clueChar = dailyWordCharArray[clueCharIndex];

                    switch (clueChar)
                    {
                        case 'å':
                            message = "Ledtråd 1, ordet innehåller bokstaven: " + "å";
                            break;
                        case 'ä':
                            message = "Ledtråd 1, ordet innehåller bokstaven: " + "ä";
                            break;
                        case 'ö':
                            message = "Ledtråd 1, ordet innehåller bokstaven: " + "ö";
                            break;

                        default:
                            message = "Ledtråd 1, ordet innehåller bokstaven: " + clueChar;
                            break;
                    }

                    showHiddenChar = true;

                   

                   await ctx.Channel.SendMessageAsync(message + '\n' + DisplayHiddenWord(ctx)).ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    await ctx.Channel.SendMessageAsync(e.Message + clueCharIndex).ConfigureAwait(false);
                }

            }
            else if (!gameHasStarted)
            {
                await ctx.Channel.SendMessageAsync("Du måste starta ett spel för att få en ledtråd. Se ?help wordgame").ConfigureAwait(false);
            }





        }

        [Command("clue2")]
        [Description("Visar, om tillgängligt, en definition av ordet.")]
        public async Task Clue2(CommandContext ctx)

        {
            if (gameHasStarted)
            {
                if(word.Definition.Equals(""))
                {
                    await ctx.Channel.SendMessageAsync("Ledtråd 2, definiton av ordet: Hittade ingen definition").ConfigureAwait(false);
                }
                else
                {
                    await ctx.Channel.SendMessageAsync("Ledtråd 2, definiton av ordet: " + word.Definition).ConfigureAwait(false);
                }
               
            }
            else
            {
                await ctx.Channel.SendMessageAsync("Du måste starta ett spel för att få en ledtråd. Se ?help wordgame").ConfigureAwait(false);
            }


        }

        [Command("clue3")]
        [Description("Visar, om tillgängligt, en synonym till ordet.")]
        public async Task Clue3(CommandContext ctx)

        {
            if (gameHasStarted)
            {
                if (word.Synonyms[0].Equals(""))
                {
                    await ctx.Channel.SendMessageAsync("Ledtråd 3, synonym: Hittade ingen synonym").ConfigureAwait(false);
                }
                else
                {
                    await ctx.Channel.SendMessageAsync("Ledtråd 3, synonym: " + word.Synonyms[0]).ConfigureAwait(false);
                }
                
            }
            else
            {
                await ctx.Channel.SendMessageAsync("Du måste starta ett spel för att få en ledtråd. Se ?help wordgame").ConfigureAwait(false);
            }


        }


        private string DisplayHiddenWord(CommandContext ctx)
        {
            string message = string.Empty;
            List<DiscordEmoji> emojis = new List<DiscordEmoji>();



            startingChar = wordGame.DailyWord.ToCharArray()[0];

            emojis.Insert(0, DiscordEmoji.FromName(ctx.Client, ":regional_indicator_" + startingChar + ":"));

            for (int i = 0; i < wordGame.DailyWord.Length - 1; i++)
            {

                
                emojis.Add((DiscordEmoji.FromName(ctx.Client, ":blue_square:")));

            }

            if (showHiddenChar)
            {
                for(int i = 0; i < wordGame.DailyWord.Length; i++)
                {
                    switch (wordGame.DailyWord[i])
                    {
                        case 'å':
                            clueChar = 'a';
                            break;
                        case 'ä':
                            clueChar = 'a';
                            break;
                        case 'ö':
                            clueChar = 'o';
                            break;
                    }
                }
                

                emojis.RemoveAt(clueCharIndex);
                emojis.Insert(clueCharIndex, DiscordEmoji.FromName(ctx.Client, ":regional_indicator_" + clueChar + ":"));
            }




            foreach (DiscordEmoji emoji in emojis)
            {
                
                message += emoji + " ";              

            }

            return message;
        }

        private string DisplayWord(CommandContext ctx)
        {
            string message = string.Empty;
            List<DiscordEmoji> emojis = new List<DiscordEmoji>();
            List<char> word = wordGame.DailyWord.ToList();


            foreach(char c in word)
            {
                string emoji;


                if (c.Equals('å') || c.Equals('ä'))
                {
                    emoji = ":regional_indicator_a:";
                }
                else if (c.Equals('ö'))
                {
                    emoji = ":regional_indicator_o:";
                }
                else
                {
                    emoji = ":regional_indicator_" + c + ":";
                }


                try
                {
                    emojis.Add(DiscordEmoji.FromName(ctx.Client, emoji));
                }
                catch (Exception e)
                {
                    message = c + " " + e.Message;
                }
            }

            foreach (DiscordEmoji emoji in emojis)
            {


                message += emoji + " ";



            }

            return message;


        }



        private async Task ResetGame()
        {
            word = null;
            wordGame.GenerateNewWord();
            showHiddenChar = false;
            clueChar = null;
            startingChar = null;
            word = await WordGame.GetWordAsync(wordGame.DailyWord);
        }




    }




}
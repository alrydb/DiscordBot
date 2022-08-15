using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscordBot.Models;

namespace DiscordBot
{
    public class WordGame
    {
        public WordGame()
        {
            DailyWord = wordList[rand.Next(wordList.Length)];
        }

        static HttpClient client = new HttpClient();

        private string[] wordList = { "hund", "anka", "kossa" };
        Random rand = new Random();
        public string DailyWord { get; private set; } 


       public static async Task<Word> GetWordAsync(string dailyWord)
        {
            //SetUpClient();

            Word word = null;
            HttpResponseMessage response = await client.GetAsync("https://synonymord.se/api/?q=" + dailyWord);
            if (response.IsSuccessStatusCode)
            {
                word = await response.Content.ReadAsAsync<Word>();
            }
            else
            {
                word = new Word
                {
                    Definition = "lorem ipsum hundum"

                };
            }
            return word;
        }


        public void SetDailyWord()
        {
            DailyWord = wordList[rand.Next(wordList.Length + 1 )];
        }
    }
}

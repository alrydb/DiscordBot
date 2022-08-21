using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscordBot.Models;
using Newtonsoft.Json;

namespace DiscordBot
{
    public class WordGame
    {
        public WordGame()
        {
           //LoadWords();
            //DailyWord = wordList[rand.Next(wordList.Length)];
            //InitalizeGame();
            
        }

        static HttpClient client = new HttpClient();





        private List<string> wordList = new List<string>();
       
        public string DailyWord { get; private set; } 


       public static async Task<Word> GetWordAsync(string dailyWord)
        {

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





        private async Task LoadWords()
        {
            var json = string.Empty;

            using (var fs = File.OpenRead("wordlist.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                
            json = await sr.ReadToEndAsync().ConfigureAwait(false);

            WordList words = JsonConvert.DeserializeObject<WordList>(json);

           for(int i = 0; i < words.Words.Length; i++)
            {
                wordList.Add(words.Words[i]);
            }
        }

        public async Task InitalizeGame()
        {
            await LoadWords();
            GenerateNewWord();
        }

        public void GenerateNewWord()
        {
            Random rand = new Random();
            DailyWord = wordList[rand.Next(wordList.Count)];
        }


    }
}

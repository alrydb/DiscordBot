using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Models
{
    public class Word
    {
        public string[]? Synonyms { get; set; } 
        public string? Definition { get; set; }
        public string? Exmaples { get; set; }
        public string? Idiom { get; set; }
    }
}

using System;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace Regex_4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var httpClient = new HttpClient();
            string htmlCode = httpClient.GetStringAsync(@"https://store.steampowered.com/app/553420/TUNIC/").Result;
            Match review = Regex.Match(htmlCode, @"((?:game_review_summary).\w+"">)(\w* ?\w*)");
            string score = review.Groups[2].Value;
            Console.WriteLine($"The current rating of the game Tunic is {score}");
        }
    }
}

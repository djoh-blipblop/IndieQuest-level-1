using System;
using System.Collections.Generic;

namespace Dictionaries
{
    internal class Program
    {
        static void generateQuestion()
        {
            
            //Set up dictionaries
            var random = new Random();
            var winterGames = new Dictionary<int, string>()
            {
                {2002, "United States"},
                {2006, "Italy" },
                {2010, "Canada" },
                {2014, "Russia" },
                {2018, "South Korea" },
                {2022, "China" },
            };

            var summerGames = new Dictionary<int, string>()
            {
                {2000, "Australia"},
                {2004, "Greece"},
                {2008, "China" },
                {2012, "United Kingdom" },
                {2016, "Brazil" },
                {2020, "Japan" }
            };

            //Randomize question (summer or winter?)
            for (int i = 0; i < 1; i++)
            { 
            int type = random.Next(1, 3);

            // Randomize which year
            int summerYear = 2000 + random.Next(summerGames.Count) * 4;
            int winterYear = 2002 + random.Next(winterGames.Count) * 4;

              

                if (type == 1)
                {
                    Console.WriteLine($"Which country hosted the olympic summer games in {summerYear}?");
                    string answer = Console.ReadLine();
                    string correctAnswer = summerGames[summerYear];

                    if (answer.ToLowerInvariant() == correctAnswer.ToLowerInvariant())
                    {
                        Console.WriteLine("Correct!");
                    }

                    else
                    {
                        Console.WriteLine($"Incorrect! The olympic summer games in {summerYear} where hosted in {correctAnswer}");
                    }
                    summerGames.Remove(summerYear);
            }
                if (type == 2)
                {
                    Console.WriteLine($"Which country hosted the olympic winter games in {winterYear}?");
                    string answer = Console.ReadLine();
                    string correctAnswer = winterGames[winterYear];

                    if (answer.ToLowerInvariant() == correctAnswer.ToLowerInvariant())
                    {
                        Console.WriteLine("Correct!");
                    }

                    else
                    {
                        Console.WriteLine($"Incorrect! The olympic winter games in {winterYear} where hosted in {correctAnswer}");
                    }
                    winterGames.Remove(winterYear);
                }
            }

        }


        static void Main(string[] args)
        {
            for (int i = 0; i < 5; i++)
            {
                generateQuestion();
            }
        }
    }
}


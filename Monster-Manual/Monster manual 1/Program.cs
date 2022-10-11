using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Monster_manual_1
{
    internal class Program
    {
        static void DisplayMonster(MonsterEntry monster)
        {
            Console.WriteLine("-----------------------");
            Console.WriteLine($"Name: {monster.Name}");
            Console.WriteLine($"Type: {monster.Description}");
            Console.WriteLine($"Aligment: {monster.Alignment}");
            Console.WriteLine($"Default HP: {monster.HPDefault}");
            Console.WriteLine($"Rolled HP: {monster.HPRoll}");
            Console.WriteLine($"Armor Class: {monster.Armor.Class}");

            if (monster.Armor.Type != "")
            {
                Console.WriteLine($"Armor type: {monster.Armor.Type}");
            }

            Console.WriteLine($"Speed: {@monster.Speed}");

            if (monster.BurrowingSpeed > 0)
            {
                Console.WriteLine($"Burrowing Speed: {monster.BurrowingSpeed}");
            }

            if (monster.FlyingSpeed > 0)
            {
                Console.WriteLine($"Flying Speed: {monster.FlyingSpeed}");
            }

            if (monster.SwimmingSpeed > 0)
            {
                Console.WriteLine($"Swimming Speed: {monster.SwimmingSpeed}");
            }

            if (monster.ClimbingSpeed > 0)
            {
                Console.WriteLine($"Climbing Speed: {monster.ClimbingSpeed}");
            }

            Console.WriteLine($"Challenge Rating: {monster.ChallengeRating}");
            Console.WriteLine($"XP Value: {monster.XPValue}");
            Console.WriteLine();
            return;
        }

        class MonsterEntry
        {
            public string Name;
            public string Description;
            public string Alignment;
            public int HPDefault;
            public string HPRoll;
            public string Speed;
            public int BurrowingSpeed;
            public int FlyingSpeed;
            public int SwimmingSpeed;
            public int ClimbingSpeed;
            public double ChallengeRating;
            public int XPValue;
            public ArmorInformation Armor;
        }

        class ArmorInformation
        {
            public int Class;
            public string Type;
        }

        static void Main(string[] args)
        {
            /*Setting up the lists etc.*/
            string monsterManual = File.ReadAllText("MonsterManual.txt");

            var monsters = new List<MonsterEntry>();

            /*Setting up the Regex pattern */
            MatchCollection matches = Regex.Matches(monsterManual,
                @"(.*)\n" + /* Monster name*/
                @"(.*), (.*)\n" + /*Description and Alignment*/
                @"Hit Points: (\d*)(?: \((.*)\))? ?\n" + /*Hitpoints and then Rolled HitPoints*/
                @"Armor Class: (\d*)(?: \((.*)\))? ?\n" + /*Armor class and then eventual armor type*/
                @"Speed: (.*)\n" + /*Full line of speed(s), for now */
                @"Challenge Rating: (\d+)(?:\/(\d+))? \((\d*,?\d*?) XP\)\n" /*Challenge Rating and XP value */
                );

            /* Adding the data from the captures into the MonsterEntry class */
            foreach (Match match in matches)
            {
                MonsterEntry monster = new MonsterEntry
                {
                    Name = match.Groups[1].Value,
                    Description = match.Groups[2].Value,
                    Alignment = match.Groups[3].Value,
                    HPDefault = Convert.ToInt32(match.Groups[4].Value),
                    HPRoll = match.Groups[5].Value,
                    /*ArmorClass = Convert.ToInt32(match.Groups[6].Value),
                    ArmorType = match.Groups[7].Value,*/
                    Armor = new ArmorInformation
                    {
                        Class = Convert.ToInt32(match.Groups[6].Value),
                        Type = match.Groups[7].Value,
                    },
                    XPValue = Convert.ToInt32(match.Groups[11].Value.Replace(",", "")), //The replace is needed because XP sometimes includes a comma (ie 5,900).
                    Speed = match.Groups[8].Value,
                };
                monsters.Add(monster);

                monster.ChallengeRating = Convert.ToDouble(match.Groups[9].Value);

                if (match.Groups[10].Success)
                {
                    double divisor = Convert.ToDouble(match.Groups[10].Value);
                    monster.ChallengeRating /= divisor;
                }

                /*string speedLine = match.Groups[8].Value;
                string regularSpeed = @"Speed: (\d*)";
                string burrowingSpeed = @"burrow (\d*)";
                string flyingSpeed = @"fly (\d*)";
                string swimmingSpeed = @"swim (\d*)";
                string climbingSpeed = @"climb (\d*)";
                string hover = @"hover";

                Regex.Match(speedLine, regularSpeed);
                monster.Speed = Convert.ToInt32(match.Groups[regularSpeed].Value);*/
            }

            /*Begin writing the user interface*/
            bool quit = false;
            string title = "MONSTER MANUAL";
            string searchByName = "Enter a query to seach monsters by name:";
            Console.WriteLine(title);
            Console.WriteLine("-----------------------------------");

            while (quit == false)
            {
                Console.WriteLine();
                Console.WriteLine(searchByName);
                string searchQuery = Console.ReadLine();
                var searchResults = new List<MonsterEntry>();

                foreach (MonsterEntry monster in monsters)
                {
                    if (Regex.IsMatch(monster.Name, searchQuery, RegexOptions.IgnoreCase))
                    {
                        searchResults.Add(monster);
                    }
                }

                if (searchResults.Count == 1)
                {
                    MonsterEntry selectedMonster = searchResults[0];
                    DisplayMonster(selectedMonster);

                }

                if (searchResults.Count > 1)
                {
                    Console.WriteLine();
                    Console.WriteLine("Which monster do did you want to look up?");
                    Console.WriteLine();
                    for (int i = 0; i < searchResults.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}: {searchResults[i].Name}");
                    }

                    Console.WriteLine("Enter number:");
                    string selection = Console.ReadLine();
                    int selected = Convert.ToInt32(selection) - 1;
                    MonsterEntry selectedMonster = searchResults[selected];

                    Console.WriteLine($"Displaying information for {selectedMonster.Name}");
                    Console.WriteLine("-------------------");
                    DisplayMonster(selectedMonster);
                }

                if (searchResults.Count == 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("No monsters matching that search were found, please try again");
                }
            }
        }
    }
}

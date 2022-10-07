using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Monster_manual_1
{
    internal class Program
    {
        class MonsterEntry
        {
            public string Name;
            public string Description;
            public string Alignment;
            public string HPDefault;
            public string HPRoll;
            public string ArmorClass;
            public string ArmorType;
            public string Speed;
            public string BurrowingSpeed;
            public string FlyingSpeed;
            public string SwimmingSpeed;
            public string ChallengeRating;
            public string XPValue;

        }
        static void Main(string[] args)
        {
            /*Setting up the lists etc.*/
            string[] monsterManualLines = File.ReadAllLines("MonsterManual.txt");
            string monsterManual = File.ReadAllText("MonsterManual.txt");

            var monsters = new List<MonsterEntry>();

            /*Setting up the Regex pattern */
            MatchCollection matches = Regex.Matches(monsterManual,
                @"\n\n(.*)\n" + /* Monster name*/
                @"(.*), (.*)\n" + /*Description and Alignment*/
                @"Hit Points: (\d*) \((.*)\) ?\n" + /*Hitpoints and then Rolled HitPoints*/
                @"Armor Class: (\d*)(?: \((.*)\))? ?\n" + /*Armor class and then eventual armor type*/
                @""

            );

            /* Adding the data from the captures into the MonsterEntry class */
            foreach (Match match in matches)
            {
                monsters.Add(new MonsterEntry
                {
                    Name = match.Groups[1].Value,
                    Description = match.Groups[2].Value,
                    Alignment = match.Groups[3].Value,
                    HPDefault = match.Groups[4].Value,
                    HPRoll = match.Groups[5].Value,
                    ArmorClass = match.Groups[6].Value,
                    ArmorType = match.Groups[7].Value,
                    Speed = match.Groups[8].Value,
                    BurrowingSpeed = match.Groups[9].Value,
                    FlyingSpeed = match.Groups[10].Value,
                    SwimmingSpeed = match.Groups[11].Value,
                    ChallengeRating = match.Groups[12].Value,
                    XPValue = match.Groups[13].Value,
                }); ;
            }


            /* Test to see that everything gets recorded properly, remove later */
            foreach (MonsterEntry monster in monsters)
            {
                Console.WriteLine(monster.Name);
                Console.WriteLine(monster.Description);
                Console.WriteLine(monster.Alignment);
                Console.WriteLine(monster.HPDefault);
                Console.WriteLine(monster.HPRoll);
                Console.WriteLine(monster.ArmorClass);
                Console.WriteLine(monster.ArmorType);
                Console.WriteLine(monster.Speed);
                Console.WriteLine(monster.BurrowingSpeed);
                Console.WriteLine(monster.FlyingSpeed);
                Console.WriteLine(monster.SwimmingSpeed);
                Console.WriteLine(monster.ChallengeRating);
                Console.WriteLine(monster.XPValue);
                Console.WriteLine();
            }
        }
    }
}

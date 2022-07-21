using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Regex_3____1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] monsterManualArray = File.ReadAllLines("MonsterManual.txt");
            string monsterManual = File.ReadAllText("MonsterManual.txt");

            var namesByAlignment = new List<string>[3, 3];
            var namesOfUnaligned = new List<string>();
            var namesOfAnyAlignment = new List<string>();
            var namesOfSpecialCases = new List<string>();

            for (int axis1 = 0; axis1 < 3; axis1++)

                for (int axis2 = 0; axis2 < 3; axis2++)

                    namesByAlignment[axis1, axis2] = new List<string>();



            MatchCollection match = Regex.Matches(monsterManual, @"((chaotic)|(neutral)|(lawful)) ((evil)|neutral)|(good)");

            var axis1Values = new[] { "chaotic", "neutral", "lawful" };
            var axis2Values = new[] { "evil", "neutral", "good" };

            string axis1Text = match.Groups[1].Value;
            string axis2Text = match.Groups[2].Value;
            int axis1 = Array.IndexOf(axis1Values, axis1Text);
            int axis2 = Array.IndexOf(axis2Values, axis2Text);
            namesByAlignment[axis1, axis2].Add(monsterName);




            for (int i = 0; i < monsterManualArray.Length; i++)
            {
                if (Regex.IsMatch(monsterManualArray[i], @"(unaligned)"))
                {
                    namesOfUnaligned.Add(monsterManualArray[i - 1]);
                }
                else if (Regex.IsMatch(monsterManualArray[i], @"(any alignment)"))
                {
                    namesOfAnyAlignment.Add(monsterManualArray[i - 1]);
                }
            }

            Console.WriteLine("The names of monsters that are unaligned are:");
            Console.WriteLine("---------------------------------------------");
            foreach (string name in namesOfUnaligned)
            {
                Console.WriteLine(name);
            }

            Console.WriteLine();

            Console.WriteLine("The names of monsters that can be of any alignment are:");
            Console.WriteLine("-------------------------------------------------------");
            foreach (string name in namesOfAnyAlignment)
            {
                Console.WriteLine(name);
            }

        }
    }
}



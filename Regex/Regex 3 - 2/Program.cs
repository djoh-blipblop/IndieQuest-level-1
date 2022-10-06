using System;

namespace Regex_3___2
{
    internal class Program
    {
        static int DiceRoll(int numberOfRolls, int diceSides, int fixedBonus = 0)
        {
            var random = new Random();
            int sum = 0;
            for (int i = 0; i < numberOfRolls; i++)
            {
                sum += random.Next(1, diceSides + 1);
            }
            return sum += fixedBonus;
        }
        static void Main(string[] args)
        {


            int sumOfRolls = DiceRoll(6, 12, 1);
            Console.WriteLine(sumOfRolls);
        }
    }
}

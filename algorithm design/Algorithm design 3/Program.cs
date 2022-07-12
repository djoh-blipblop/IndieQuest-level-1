using System;
using System.Collections.Generic;

namespace Algorithm_design_3
{
    internal class Program
    {
        // Setting the height and width of the map
        static int width = 100;
        static int height = 20;

        //initializing lists that will store the values we will generate later. We use them for drawing the map.
        static List<int> roadLeftRight = new();
        static List<int> river = new();
        static List<int> wall = new();

        static void DrawCurve(List<int> curves, int position)
        {
            /* This section checks how a map element curves and ensures that the DrawMap method 
             * draws the appropriate symbol*/

            if (curves[position + 1] == curves[position] + 1)
            {
                Console.Write(@"\");
                return;
            }
            else if (curves[position + 1] == curves[position] - 1)
            {
                Console.Write("/");
                return;
            }
            else
            {
                Console.Write("|");
                return;
            }
        }
        static List<int> GenerateCurve(List<int> curves, int position, int curveChance)
        {
            /* This method generates a list of integers that determine the curvature of a map element. 
             * The user should set the curve chance when they call the method.
             * a curve chance of 4 will mean that 50% of the time the road will not curve,
             * 25% of the time it curves left, 25% of the time it curves right.
             * only the two last digits in the chance range will cause the element to curve
             * so if you pass int 8 into curveChance, only on a 6 or 7 will the road curve
             * (8 is the exlusive upper bound of the random.Next method)*/

            var random = new Random();
            int currentCurveX = position;
            var curveValues = new List<int>();

            for (int y = 0; y < height; y++)
            {
                int chance = random.Next(curveChance);
                {
                    if (chance >= curveChance - 2)
                    {
                        if (chance == curveChance - 1)
                        {
                            ++currentCurveX;
                        }
                        else
                        {
                            --currentCurveX;
                        }
                    }
                    curveValues.Add(currentCurveX);
                }
            }
            return curveValues;

        }
        static void CreateMap()
        {
            var random = new Random();

            // Generating Wall
            wall = GenerateCurve(wall, width * 1 / 4, 8);

            // Generating the river
            river = GenerateCurve(river, width * 3 / 4, 4);


            // Generating the road that goes left to right
            int roadStartY = height / 2;
            int currentRoadY = roadStartY;
            /* This section checks if the current position is close to another map element,
             * and if so, set a boolean to tell the later section that this position is close to another map element*/
            for (int x = 0; x < width; x++)
            {
                int roadCurveChance = random.Next(8);
                bool closeToObject = false;
                for (int n = -3; n <= 5; n++)
                {
                    if (river.Contains(x + n) || wall.Contains(x + n))
                    {
                        closeToObject = true;
                        break;
                    }
                }
                if (roadCurveChance >= 6 && !closeToObject) // if we are not close to an object, the road can curve.
                {
                    if (roadCurveChance == 6)
                    {
                        roadLeftRight.Add(++currentRoadY);
                    }
                    else
                    {
                        roadLeftRight.Add(--currentRoadY);
                    }
                }
                else
                {
                    roadLeftRight.Add(currentRoadY);
                }
            }
            return;
        }
        static void DrawMap()
        {
            var random = new Random();
            string title = ("ADVENTURE MAP");
            string turret = ("[]");

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    /*Drawing the borders by checking if the current position "x" is equal to 0 or the width. 
                     * similar with "y", is it 0 or the height?*/

                    if (x == 0 && y == 0 || x == width - 1 && y == height - 1 || x == width - 1 && y == 0 || x == 0 && y == height - 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("+");
                        continue;
                    }
                    else if (x == 0 || x == width - 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("|");
                        continue;
                    }
                    else if (y == 0 || y == height - 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("-");
                        continue;
                    }

                    /* Positioning and Drawing the title, and making sure that the title doesnt "push" 
                     * the map positions weirdly by moving the current x position in the algorithm
                     * forward based on the lenght of the string*/

                    if (x == width / 2 - title.Length / 2 && y == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(title);
                        x += title.Length - 1;
                        continue;
                    }

                    // Drawing the bridge, we check if the road and river are intersecting, or will intersect in the coming coordinates.
                    if (x >= river[y] - 1 && x <= river[y] + 3 && y + 1 == roadLeftRight[x] || x >= river[y] - 1 && x <= river[y] + 3 && y - 1 == roadLeftRight[x])
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("=");
                        continue;
                    }

                    //Drawing the road that leads down by checking if we are 5 steps before from the river and past the road on the y axis.
                    if (x == river[y] - 5 && y > roadLeftRight[x])
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("#");
                        continue;
                    }

                    //Drawing the road that goes left to right
                    if (roadLeftRight[x] == y)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("#");
                        continue;
                    }

                    //Drawing the turrets, using a similar technique as when drawing the title to ensure correct positioning.
                    if (x == wall[y] && y + 1 == roadLeftRight[x] || x == wall[y] && y - 1 == roadLeftRight[x])
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write(turret);
                        x += turret.Length - 1;
                        continue;
                    }

                    //Drawing the wall
                    if (x >= wall[y] && x <= wall[y] + 1)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        DrawCurve(wall, y);
                        continue;
                    }

                    // Drawing the trees
                    var trees = new List<string> { "T", "X", "Z", "(", ")" };

                    if (x >= 1 && y >= 1 && x <= width / 4 && y != height - 1)
                    {
                        int chance = width / 4;

                        if (chance <= random.Next(width / 4, width) - (x * 4))
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write($"{trees[random.Next(trees.Count)]}");
                            continue;
                        }
                    }

                    //Drawing the river
                    if (x >= river[y] && x <= river[y] + 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        DrawCurve(river, y);
                        continue;
                    }

                    //Filling in space thats not drawn with blanks
                    else
                    {
                        Console.Write(" ");
                        continue;
                    }
                }
                Console.WriteLine(); //ensuring line breaks at the end of the x-axis
            }
        }
        static void Main(string[] args)
        {
            CreateMap();
            DrawMap();
        }
    }
}




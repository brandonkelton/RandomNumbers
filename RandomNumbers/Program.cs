using System;
using System.Collections.Generic;

namespace RandomNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("1) Generate Random Numbers with Mersenne Twist Algorithm");
                Console.WriteLine("2) Generate Random Numbers with C#'s Subtractive Algorithm");
                Console.WriteLine();
                Console.WriteLine("--Press <ESCAPE> to Exit--");
                Console.WriteLine();
                Console.Write("Your Selection: ");
                var result = Console.ReadKey();
                if (result.Key == ConsoleKey.Escape)
                {
                    break;
                }
                switch (result.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        RequestNumbers(false);
                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        RequestNumbers(true);
                        break;
                    default:
                        break;
                }
            }
        }

        private static void RequestNumbers(bool isBuiltInAlgorithm = true)
        {
            Console.Clear();

            while (true)
            {
                Console.WriteLine();

                Console.Write("Input a seed (input nothing for auto-seed or Q to quit): ");
                var seedString = Console.ReadLine();
                if (seedString.ToUpper() == "Q")
                {
                    break;
                }
                Console.WriteLine();
                Console.WriteLine();

                if (seedString.Trim() == "")
                {
                    if (isBuiltInAlgorithm)
                    {
                        RunBuiltInAlgorithm();
                    } else
                    {
                        RunMercenneTwist();
                    }
                }
                else
                {
                    int seed;
                    if (int.TryParse(seedString, out seed))
                    {
                        if (isBuiltInAlgorithm)
                        {
                            RunBuiltInAlgorithm(seed);
                        }
                        else
                        {
                            RunMercenneTwist(seed);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid Input!");
                    }
                }
            }
        }

        private static void RunMercenneTwist(int? seed = null)
        {
            MersenneTwist generator;
            if (seed.HasValue)
            {
                generator = new MersenneTwist(seed.Value);
            }
            else
            {
                generator = new MersenneTwist();
            }

            Console.WriteLine("Generating 1,000,000 Random Numbers...");
            var numbers = new List<ulong>();
            for (int i = 0; i < 1000000; i++)
            {
                var num = generator.Next();
                numbers.Add(num);
            }
            var test = new DistributionTest(numbers.ToArray());
            test.Run();
        }

        private static void RunBuiltInAlgorithm(int? seed = null)
        {
            Random generator;
            if (seed.HasValue)
            {
                generator = new Random(seed.Value);
            }
            else
            {
                generator = new Random();
            }

            Console.WriteLine("Generating 1,000,000 Random Numbers...");
            var numbers = new List<ulong>();
            for (int i = 0; i < 1000000; i++)
            {
                var num = generator.Next();
                numbers.Add((ulong)num);
            }
            var test = new DistributionTest(numbers.ToArray());
            test.Run();
        }
    }
}

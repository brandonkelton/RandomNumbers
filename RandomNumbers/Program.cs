using System;

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
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine();

                Console.Write("Press Q to exit. Input nothing for auto-seed or input a seed: ");
                var result = Console.ReadLine();
                if (result.ToUpper() == "Q")
                {
                    break;
                }

                Console.WriteLine();
                Console.WriteLine();

                if (result.Trim() == "")
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
                    if (int.TryParse(result, out seed))
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

            for (int i = 0; i < 10; i++)
            {
                var num = generator.Next();
                Console.WriteLine(num);
            }
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

            for (int i = 0; i < 10; i++)
            {
                var num = generator.Next();
                Console.WriteLine(num);
            }
        }
    }
}

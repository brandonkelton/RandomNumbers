using System;

namespace RandomNumbers
{
    class Program
    {
        static void Main(string[] args)
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
                    Run();
                } 
                else
                {
                    ulong seed;
                    if (ulong.TryParse(result, out seed))
                    {
                        Run(seed);
                    } else
                    {
                        Console.WriteLine("Invalid Input!");
                    }
                }
            }
        }

        private static void Run(ulong? seed = null)
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
    }
}

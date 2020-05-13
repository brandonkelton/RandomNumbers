using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RandomNumbers
{
    public class DistributionTest
    {
        private ulong[] numbers;

        public DistributionTest(ulong[] numbers)
        {
            this.numbers = numbers;
        }

        public void Run()
        {
            var min = numbers.Min();
            var max = numbers.Max();
            var range = max - min;
            var buckets = Enumerable.Range(0, 10).Select(i => new Bucket()).ToArray();

            foreach (var number in numbers)
            {
                if (number < range * .1) buckets[0].Put(number);
                if (number >= range * .1 && number < range * .2)
                    buckets[1].Put(number);
                if (number >= range * .2 && number < range * .3)
                    buckets[2].Put(number);
                if (number >= range * .3 && number < range * .4)
                    buckets[3].Put(number);
                if (number >= range * .4 && number < range * .5)
                    buckets[4].Put(number);
                if (number >= range * .5 && number < range * .6)
                    buckets[5].Put(number);
                if (number >= range * .6 && number < range * .7)
                    buckets[6].Put(number);
                if (number >= range * .7 && number < range * .8)
                    buckets[7].Put(number);
                if (number >= range * .8 && number < range * .9)
                    buckets[8].Put(number);
                if (number >= range * .9) buckets[9].Put(number);
            }

            Console.WriteLine();
            for (var i=0; i<buckets.Length; i++)
            {
                Console.WriteLine($"Bucket {i + 1}: {buckets[i].Count()}");
            }
            Console.WriteLine();

            var mean = buckets.Select(b => b.Count()).Average();
            var squaredDifferences = buckets.Select(b => Math.Pow(b.Count() - mean, 2)).ToList();
            var variance = squaredDifferences.Sum() / buckets.Count();
            var stdDev = Math.Sqrt(variance);
            var averagePercentDifferenceFromMean = stdDev / mean;

            Console.WriteLine($"Mean: {mean}");
            Console.WriteLine($"Variance: {variance}");
            Console.WriteLine($"Standard Deviation: {stdDev}");
            Console.WriteLine($"Difference from Mean: {averagePercentDifferenceFromMean}");
            Console.WriteLine();
        }
    }
}

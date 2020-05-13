using System;
using System.Collections.Generic;
using System.Text;

namespace RandomNumbers
{
    public class Bucket
    {
        private List<ulong> Numbers { get; } = new List<ulong>();

        public void Put(ulong number)
        {
            Numbers.Add(number);
        }

        public int Count()
        {
            return Numbers.Count;
        }
    }
}

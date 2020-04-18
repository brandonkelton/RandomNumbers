using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RandomNumbers
{
    public class MersenneTwist
    {
        private const int w_wordSizeInBits = 64;
        private const int n_degreeOfRecurrence = 312;
        private const int m_middleWordOffset = 156;
        private const int r_wordSeparationPoint = 31;
        private const ulong a_coefficientsOfRationalNormalFormTwistMatrix = 0xB5026F5AA96619E9;
        private const ulong b_temperingBitMask = 0x71D67FFFEDA60000;
        private const ulong c_temperingBitMask = 0xFFF7EEE000000000;
        private const int s_temperingBitShift = 17;
        private const int t_temperingBitShift = 37;
        private const int u_temperingBitShiftMask = 29;
        private const ulong d_temperingBitShiftMask = 0x5555555555555555;
        private const int l_temperingBitShiftMask = 43;
        private const ulong f_generatorParameter = 6364136223846793005;
        private const ulong lowerMask = (1UL << r_wordSeparationPoint) - 1;
        private const ulong upperMask = ~lowerMask;

        private readonly ulong[] MT_GeneratorState = new ulong[n_degreeOfRecurrence];
        private int index = n_degreeOfRecurrence + 1;

        public MersenneTwist()
        {
            InitializeGenerator(Guid.NewGuid().GetHashCode());
        }

        public MersenneTwist(int seed)
        {
            InitializeGenerator(seed);
        }

        private void InitializeGenerator(int seed)
        {
            index = n_degreeOfRecurrence;
            MT_GeneratorState[0] = (ulong) seed;
            for (int i=1; i<(n_degreeOfRecurrence - 1); i++)
            {
                MT_GeneratorState[i] = f_generatorParameter * (MT_GeneratorState[i-1] ^ (MT_GeneratorState[i-1] >> (w_wordSizeInBits-2))) + (ulong)i;
            }
        }

        public ulong Next()
        {
            if (index >= n_degreeOfRecurrence)
            {
                Twist();
            }

            var y = MT_GeneratorState[index];
            y = y ^ ((y >> u_temperingBitShiftMask) & d_temperingBitShiftMask);
            y = y ^ ((y << s_temperingBitShift) & b_temperingBitMask);
            y = y ^ ((y << t_temperingBitShift) & c_temperingBitMask);
            y = y ^ (y >> l_temperingBitShiftMask);

            index++;

            return y;
        }

        private void Twist()
        {
            for (int i=0; i < n_degreeOfRecurrence-1; i++)
            {
                var x = (MT_GeneratorState[i] & upperMask) + (MT_GeneratorState[(i + 1) % n_degreeOfRecurrence] & lowerMask);
                var xA = x >> 1;
                if ((x % 2) != 0)
                {
                    xA = xA ^ a_coefficientsOfRationalNormalFormTwistMatrix;
                }
                MT_GeneratorState[i] = MT_GeneratorState[(i + m_middleWordOffset) % n_degreeOfRecurrence] ^ xA;
            }

            index = 0;
        }
    }
}

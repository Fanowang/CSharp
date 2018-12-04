using System;
using System.Linq;

namespace CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] myarray = new int[14] { 0, 2, 4, 1, 5, 7, 4, 2, 1, 14, 12, 14, 5, 455 };
            Console.WriteLine(BinarySearch(12, myarray));

            int[] test = new int[8] { 1, 0, 0, 0, 0, 1, 0, 0 };
            int[] result = new int[8];
            result = cellCompete(test, 1);
            foreach (var r in result)
            {
                Console.WriteLine(r);
            }

            int[] test1 = new int[5] { 2, 3, 4, 5, 6 };
            int test2resut = generalizedGCD(5, test1);
            Console.WriteLine("test2: " + test2resut);

        }

        public static int generalizedGCD(int num, int[] arr)
        {
            // WRITE YOUR CODE HERE
            int minValue = arr.Min();
            Console.WriteLine("minvalue: " + minValue);
            int i = minValue;

            bool found = false;
            while (!found && i > 0)
            {
                int j = 0;
                double remaind = 0;
                foreach (int a in arr)
                {
                    remaind += (a % i);
                    Console.WriteLine(remaind);
                }
                if (remaind == 0)
                {
                    found = true;
                }
                else
                {
                    i--;
                }
            }
            return i;

        }
        public static int[] cellCompete(int[] states, int days)
        {
            // INSERT YOUR CODE HERE
            // INSERT YOUR CODE HERE
            int i = 0;

            while (i < days)
            {
                int[] currentstates = new int[8];
                Array.Copy(states, currentstates, 8);
                states[0] = currentstates[1] == 0 ? 0 : 1;
                states[7] = currentstates[6] == 0 ? 0 : 1;
                int j = 1;
                while (j < 7)
                {
                    states[j] = currentstates[j - 1] == currentstates[j + 1] ? 0 : 1;
                    j++;
                }
                i++;
            }
            return states;
        }
        public static bool BinarySearch(int target, int[] nums)
        {
            // See if target appears in nums

            // We think of floorIndex and ceilingIndex as "walls" around
            // the possible positions of our target, so by -1 below we mean
            // to start our wall "to the left" of the 0th index
            // (we *don't* mean "the last index").
            int floorIndex = -1;
            int ceilingIndex = nums.Length;

            // If there isn't at least 1 index between floor and ceiling,
            // we've run out of guesses and the number must not be present
            while (floorIndex + 1 < ceilingIndex)
            {
                // Find the index ~halfway between the floor and ceiling
                // We use integer division, so we'll never get a "half index"
                int distance = ceilingIndex - floorIndex;
                int halfDistance = distance / 2;
                int guessIndex = floorIndex + halfDistance;

                int guessValue = nums[guessIndex];

                if (guessValue == target)
                {
                    return true;
                }

                if (guessValue > target)
                {
                    // Target is to the left, so move ceiling to the left
                    ceilingIndex = guessIndex;
                }
                else
                {
                    // Target is to the right, so move floor to the right
                    floorIndex = guessIndex;
                }
            }

            return false;
        }
    }
}

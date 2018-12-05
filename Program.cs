using System;
using System.Linq;
using System.Collections.Generic;

namespace CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] myarray = new int[14] { 0, 2, 4, 1, 5, 7, 4, 2, 1, 14, 12, 14, 5, 455 };
            Console.WriteLine(BinarySearch(12, myarray));
            Console.WriteLine(myBinaryTreeSearch(12, myarray));

            int[] test = new int[8] { 1, 0, 0, 0, 0, 1, 0, 0 };
            int[] result = new int[8];

            Console.WriteLine(string.Join(",", cellCompete(test, 1)));
            int[] test1 = new int[5] { 2, 3, 4, 5, 6 };
            int test2resut = generalizedGCD(5, test1);
            Console.WriteLine("test2: " + test2resut);

            Console.WriteLine(string.Join(",", myCountingSort(myarray)));

            Console.WriteLine(string.Join(",", mergeSort(myarray)));

            Console.WriteLine(checkBracks("{fdsfsd{dfdssd}kls[dsdsf(fddsd)]}"));

        }

        public static bool checkBracks(string str)
        {
            var openerandcloser = new Dictionary<char, char>
            {
                {'{','}'},
                {'[',']'},
                {'(',')'}
            };

            var openers = new HashSet<char>(openerandcloser.Keys);
            var closers = new HashSet<char>(openerandcloser.Values);

            var openerstack = new Stack<char>();

            foreach (char s in str)
            {
                if (openers.Contains(s))
                {
                    openerstack.Push(s);
                }
                if (closers.Contains(s))
                {
                    if (openerstack.Count == 0)
                    {
                        return false;
                    }
                    char lastopener = openerstack.Pop();
                    if (openerandcloser[lastopener] != s)
                    {
                        return false;
                    }
                }
            }

            return openerstack.Count == 0;

        }
        public static int[] mergeSort(int[] arr)
        {
            //O(nlog2n) time
            if (arr.Length < 2)
            {
                return arr;
            }
            int halfIndex = arr.Length / 2;
            int[] leftArray = makeSubArray(arr, 0, halfIndex);
            int[] rightArray = makeSubArray(arr, halfIndex, arr.Length);

            int[] sortedleftArr = mergeSort(leftArray);
            int[] sortedrightArr = mergeSort(rightArray);

            int currentleftIndex = 0;
            int currentrightIndex = 0;

            int[] sortedArray = new int[arr.Length];

            for (int i = 0; i < sortedArray.Length; i++)
            {
                if (currentleftIndex < sortedleftArr.Length && (currentrightIndex >= sortedrightArr.Length || sortedleftArr[currentleftIndex] < sortedrightArr[currentrightIndex]))
                {
                    sortedArray[i] = sortedleftArr[currentleftIndex];
                    currentleftIndex++;
                }
                else
                {
                    sortedArray[i] = sortedrightArr[currentrightIndex];
                    currentrightIndex++;
                }
            }

            return sortedArray;

        }

        public static T[] makeSubArray<T>(T[] arr, int start, int end)
        {
            if (arr == null)
            {
                throw new ArgumentNullException(nameof(arr), "array cannot be null!");
            }
            if (start > arr.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(arr), "the start index is too big!");
            }
            if (end > arr.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(arr), "the end is too index big!");
            }
            if (start > end)
            {
                throw new ArgumentOutOfRangeException(nameof(arr), "start index cannot larger than end index!");
            }

            T[] subArray = new T[end - start];
            if (subArray.Length > 0)
            {
                Array.Copy(arr, start, subArray, 0, subArray.Length);
            }

            return subArray;


        }
        public static int[] myCountingSort(int[] arr)
        {
            //O(n) time and space
            int maxValue = arr.Max();
            int[] numsArray = new int[maxValue + 1];

            foreach (int a in arr)
            {
                numsArray[a]++;
            }
            int[] sortedArr = new int[arr.Length];
            int currentIndex = 0;
            for (int i = 0; i < numsArray.Length; i++)
            {
                int count = numsArray[i];

                for (int j = 0; j < count; j++)
                {
                    sortedArr[currentIndex] = i;
                    currentIndex++;
                }

            }

            return sortedArr;

        }
        public static int myBinaryTreeSearch(int target, int[] sortedArr)
        {
            //O(log2n) time O(n) space
            int floorIndex = -1;
            int ceilingIndex = sortedArr.Length;

            while (floorIndex + 1 < ceilingIndex)
            {
                int distance = ceilingIndex - floorIndex;
                int halfDistance = distance / 2;
                int guessIndex = floorIndex + halfDistance;

                if (sortedArr[guessIndex] == target)
                {
                    return guessIndex;
                }
                if (sortedArr[guessIndex] > target)
                {
                    ceilingIndex = guessIndex;
                }
                else
                {
                    floorIndex = guessIndex;
                }
            }
            return -1;


        }
        public static int generalizedGCD(int num, int[] arr)
        {
            // WRITE YOUR CODE HERE
            //O(n^2) time and O(n^2) space
            int minValue = arr.Min();
            Console.WriteLine("minvalue: " + minValue);
            int i = minValue;

            bool found = false;
            while (!found && i > 0)
            {
                double remaind = 0;
                foreach (int a in arr)
                {
                    remaind += (a % i);
                    //Console.WriteLine(remaind);
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

            //n time and n space
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

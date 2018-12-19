using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace CSharp
{
    public class DistinationPoint
    {
        public int x { get; private set; }
        public int y { get; private set; }
        public int Distance
        {
            get
            {
                return x * x + y * y;
            }
        }
        public DistinationPoint(int xd, int yd)
        {
            x = xd;
            y = yd;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            /* int[] myarray = new int[14] { 0, 2, 4, 1, 5, 7, 4, 2, 1, 14, 12, 14, 5, 455 };
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
            int[,] test11 = new int[3, 2] { { 1, -3 }, { 1, 2 }, { 3, 4 } };
            Console.WriteLine(ClosestXdestinations(3, test11, 1)); */

            Console.WriteLine(removeObstacle(3, 3,
             new int[,] { { 1, 1, 1 }, { 0, 0, 1 }, { 1, 9, 1 } }));

        }

        public static int nearPoint(int x, int y, int numRows, int numColumns, int[,] lot)
        {
            if (lot[x, y] == 9)
            {
                return 1;
            }
            if (lot[x, y] == 1)
            {
                int steps = 1;
                int[] values = new int[4];
                if (x > 1)
                {
                    values[0] = nearPoint(x - 1, y, numRows, numColumns, lot);
                }
                if (x < numRows)
                {
                    values[1] += nearPoint(x + 1, y, numRows, numColumns, lot);
                }
                if (y > 1)
                {
                    values[2] = nearPoint(x, y - 1, numRows, numColumns, lot);
                }
                if (y < numColumns)
                {
                    values[3] = nearPoint(x, y + 1, numRows, numColumns, lot);
                }
                int zcount = values.Where(s => s != 0).Count();
                if (zcount > 1)
                {
                    steps += values.Min();
                }
                else if (zcount == 1)
                {
                    steps += values.Max();
                }
                else
                {
                    return 0;
                }

                return steps;


            }
            else
            {
                return 0;
            }
        }
        public static int removeObstacle(int numRows, int numColumns, int[,] lot)
        {
            // WRITE YOUR CODE HERE
            if (numRows < 1 || numColumns < 1 || numRows > 1000 || numColumns > 1000)
            {
                return 0;
            }

            return nearPoint(numRows - 1, numColumns - 1, numRows, numColumns, lot);

            /* if (lot[0, 0] == 9)
            {
                return 1;
            }

            int steps = 1;
            int rowresult = 0;
            int columnresult = 0;
            if (lot[0, 0] == 1)
            {
                if (numColumns > 1 && numRows > 0)
                {
                    int[,] columnminus = new int[numRows, numColumns - 1];
                    for (int i = 0; i < numRows; i++)
                    {
                        Array.Copy(lot, i * numColumns + 1, columnminus, i * (numColumns - 1), numColumns - 1);
                    }
                    columnresult = removeObstacle(numRows, numColumns - 1, columnminus);
                }
                if (numRows > 1 && numColumns > 0)
                {
                    int[,] rowminus = new int[numRows - 1, numColumns];
                    Array.Copy(lot, numColumns, rowminus, 0, lot.Length - numColumns);
                    rowresult = removeObstacle(numRows - 1, numColumns, rowminus);
                }

                if (columnresult != 0 && rowresult != 0)
                {
                    steps += Math.Min(columnresult, rowresult);
                }
                else if (columnresult != 0 || rowresult != 0)
                {
                    steps += Math.Max(columnresult, rowresult);
                }
                else
                {
                    return 0;
                }


                return steps;

            }
            else
            {
                return 0;
            } */




        }
        public static List<List<int>> ClosestXdestinations(int numDestinations,
                                                    int[,] allLocations,
                                                    int numDeliveries)
        {
            // WRITE YOUR CODE HERE
            if (numDestinations < numDeliveries)
            {
                return null;
            }
            List<List<int>> results = new List<List<int>>();
            List<DistinationPoint> allDistance = new List<DistinationPoint>();
            int[,] test = new int[2, 2] { { 1, 2 }, { 3, 4 } };

            for (int i = 0; i < allLocations.GetLength(0); i++)
            {
                DistinationPoint distination = new DistinationPoint(allLocations[i, 0], allLocations[i, 1]);
                allDistance.Add(distination);
            }
            List<DistinationPoint> sortedList = new List<DistinationPoint>();
            sortedList = allDistance.OrderBy(s => s.Distance).ToList();
            Console.WriteLine(sortedList.Count + " | " + numDeliveries);
            for (int i = 0; i < numDeliveries; i++)
            {
                List<int> result = new List<int> { sortedList[i].x, sortedList[i].y };
                results.Add(result);
            }
            return results;


        }

        public static List<Meeting> mergeMeetings(List<Meeting> meetings)
        {
            var sortedMeetings = meetings.Select(s => new Meeting(s.StartTime, s.EndTime)).OrderBy(s => s.StartTime).ToList();
            List<Meeting> returnMeetings = new List<Meeting> { sortedMeetings[0] };
            foreach (var meeting in sortedMeetings)
            {
                var latestmeeting = returnMeetings.Last();
                if (latestmeeting.EndTime >= meeting.StartTime)
                {
                    latestmeeting.EndTime = Math.Max(latestmeeting.EndTime, meeting.EndTime);
                }
                else
                {
                    returnMeetings.Add(meeting);
                }
            }
            return returnMeetings;

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

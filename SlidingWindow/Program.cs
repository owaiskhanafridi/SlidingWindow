using System;
using System.Collections.Generic;
using System.Linq;

namespace SlidingWindow
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(Program.MaxSumSubArray(new int[] { 3, 1, 1, 2, 4, 4 }, 3));
            //Console.WriteLine(Program.FirstNegativeNumbersInWindow(new int[] { 12, -1, -7, 8, -15, 30, 16, 28 }, 3));
            //Console.WriteLine(Program.OccurenceOfAnagram("forxxorfxdofr", "for"));
            Console.WriteLine(Program.MaxOfAllSubArray(new int[] { 1, 7, 3, -1, 2, 8, 6, 1 }, 3));
            //Console.WriteLine(Program.LargestSubArrayOfSum(new int[] { 4, 1, 1, 1, 2, 3, 5 }, 5));

            Console.ReadLine();
        }

        public static int MaxSumSubArray(int[] array, int windowSize)
        {
            int start = 0;
            int arraySize = array.Length;
            int maxSum = int.MinValue;
            int sum = 0;

            for (int end = 0; end < arraySize; end++)
            {
                sum += array[end];
                
                //window size reached
                if (end - start + 1 == windowSize)
                {
                    maxSum = Math.Max(sum, maxSum);
                    //eliminate first element
                    sum -= array[start];
                    
                    //slide the window
                    start++;
                }
            }
            return maxSum;
        }

        public static int[] FirstNegativeNumbersInWindow(int[] array, int windowSize)

        {
            int start = 0;
            Queue<int> negatives = new Queue<int>();
            List<int> result = new List<int>();

            for (int end = 0; end < array.Length; end++)
            {
                if (array[end] < 0)
                    negatives.Enqueue(array[end]);

                if (end - start + 1 == windowSize)
                {
                    if (negatives.Count == 0)
                    {
                        result.Add(0);
                    }
                    else
                    {
                        result.Add(negatives.Peek());
                        if (array[start] == negatives.Peek())
                            negatives.Dequeue();

                    }
                    start++;
                }
            }

            return result.ToArray();
        }

        public static int OccurenceOfAnagram(string value, string pattern)
        {
            int start = 0;
            int windowSize = pattern.Length;
            int occurences = 0;

            Dictionary<char, int> patternOccurence = new Dictionary<char, int>();

            for (int i = 0; i < windowSize; i++)
            {
                if (patternOccurence.ContainsKey(pattern[i]))
                    patternOccurence[pattern[i]]++;
                else
                    patternOccurence.Add(pattern[i], 1);
            }

            for (int end = 0; end < value.Length; end++)
            {
                if (patternOccurence.ContainsKey(value[end]))
                    patternOccurence[value[end]]--;

                if (end - start + 1 == windowSize)
                {
                    if (patternOccurence.All(x => x.Value == 0))
                        occurences++;

                    //Refreshing values
                    if (patternOccurence.ContainsKey(value[start]))
                        patternOccurence[value[start]]++;

                    start++;
                }
            }

            return occurences;
        }

        public static int[] MaxOfAllSubArray(int[] array, int windowSize)
        {
            Queue<int> temp = new Queue<int>();
            List<int> maxList = new List<int>();
            int start = 0;

            for (int end = 0; end < array.Length; end++)
            {
                //remove all elements less than current element array[end] 
                //because those element wont be usefull in the future.
                if (temp.Count > 0)
                    temp = new Queue<int>(temp.Where(x => x > array[end]));

                //push element to temporary queue
                temp.Enqueue(array[end]);

                //When window size reached
                if (end - start + 1 == windowSize)
                {
                    //The first element of queue will always be largest in the window.
                    //Adding that to max list
                    maxList.Add(temp.Peek());

                    //If first element of temporary queue is equal to starting element, 
                    //then remove it from temp queue since window will be sliding and 
                    //new start position will be one element after that.
                    if (temp.Peek() == array[start])
                        temp.Dequeue();

                    //slide the window one step further.
                    start++;
                }
            }
            return maxList.ToArray();
        }

        public static int LargestSubArrayOfSum(int[] array, int targetSum)
        {
            int start = 0;
            int sum = 0;
            int maxWindow = int.MinValue;

            for (int end = 0; end < array.Length; end++)
            {
                sum += array[end];

                if (sum == targetSum)
                {
                    maxWindow = Math.Max(end - start + 1, maxWindow);
                }

                while (sum > targetSum)
                {
                    sum -= array[start];
                    start++;
                }
            }

            return maxWindow;

        }


    }
}

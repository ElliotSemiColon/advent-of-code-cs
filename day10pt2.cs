using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace _10._2._2
{
    class Program
    {
        static List<int> MergeSort(List<int> input) //recursive merge sort 
        {
            List<int> upper = new List<int>(), lower = new List<int>(), output = new List<int>();
            int midpoint;

            if (input.Count > 1)
            {
                midpoint = (input.Count / 2);
                lower = input.GetRange(0, midpoint);
                upper = input.GetRange(midpoint, input.Count - (midpoint));
                if (lower.Count > 1) { lower = MergeSort(lower); } //splits items, then sorts them and returns the final pair of sublists
                if (upper.Count > 1) { upper = MergeSort(upper); }
            }

            while (lower.Count > 0 && upper.Count > 0) //sorts and merges lists
            {
                if (lower[0] <= upper[0]) //looks at top items
                {
                    output.Add(lower[0]);
                    lower.RemoveAt(0); //removes top item (will have been the smallest number of both list's top items)
                }
                else
                {
                    output.Add(upper[0]);
                    upper.RemoveAt(0);
                }
            }

            while (lower.Count > 0 || upper.Count > 0)
            { //adds residual items to the end of the list 
                if (upper.Count > 0)
                {
                    output.Add(upper[0]);
                    upper.RemoveAt(0);
                }
                else
                {
                    output.Add(lower[0]);
                    lower.RemoveAt(0);
                }
            }

            return output;
        }

        static long Splitter(List<int> input)
        {

            List<int> workingList = new List<int>();
            long combinations = 1; //assumes there is at least 1 combination
            bool notAdded;

            for(int i = 0; i < input.Count - 1; i++)
            {
                notAdded = true;

                if (input[i + 1] == input[i] + 3) //if there is a gap in front of input[i] of three jolts
                {
                    workingList.Add(input[i]);
                    notAdded = false;
                    if (workingList.Count > 1) {
                        Console.WriteLine($"checking combinations in {string.Join(",", workingList)} ");
                        combinations *= StepThrough(workingList, 0); 
                    }
                    workingList = new List<int>();
                }
                if (notAdded) { workingList.Add(input[i]); }

            }

            return combinations;
            
        }

        static int StepThrough(List<int> input, int count) //totals number of valid configurations recursively, naive implementation
        {
            //largest number of valid connections will when j is 3 and i is 3, meaning there are 3 adaptors with a difference of one between each other
            int limit, temp, diff;

            if (input.Count - 1 > 3) { limit = 3; } else { limit = input.Count - 1; }

            for (int j = 1; j <= limit; j++)
            {
                diff = input[j] - input[0];
                if (diff <= 3)
                {
                    //Console.WriteLine(string.Join(",", input.GetRange(j, input.Count - j)));
                    temp = j;
                    if (diff == 3) { j = limit + 1; }
                    count = StepThrough(input.GetRange(temp, input.Count - temp), count);
                }
            }

            if (input.Count == 1) { count++; }

            return count;
        }

        static List<int> ProcessFile(string path)
        {
            string[] rawLines = File.ReadAllLines(path);
            List<int> rawList = new List<int>();
            foreach (string ln in rawLines) { rawList.Add(Int32.Parse(ln)); };

            List<int> sortedAdapters = MergeSort(rawList);
            sortedAdapters.Add(sortedAdapters[sortedAdapters.Count - 1] + 3); //adds device adaptor joltage
            sortedAdapters.Insert(0, 0); //adds wall joltage

            return sortedAdapters;
        }

        static void Main(string[] args)
        {

            List<int> sortedAdaptors = ProcessFile(@"C:\Users\ellio\Documents\programming\c#\vs code\advent of code\inputs\day10.txt");

            Console.WriteLine($"{Splitter(sortedAdaptors)} combinations");

        }
    }
}

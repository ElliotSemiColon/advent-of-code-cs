using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace _10._1
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
                //Console.WriteLine(midpoint);
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
            while (lower.Count > 0 || upper.Count > 0) { //adds residual items to the end of the list 
                if (upper.Count > 0) 
                { 
                    output.Add(upper[0]);
                    upper.RemoveAt(0);
                } else { 
                    output.Add(lower[0]);
                    lower.RemoveAt(0);
                } 
            }

            return output;
        }

        static int CountGaps(List<int> adapters) //counts the gaps of 1 and gaps of 3, then finds their product and returns it 
        {
            int ones = 0, threes = 0, previousJoltage = 0, difference;

            foreach(int joltage in adapters)
            {
                difference = joltage - previousJoltage;
                if (difference == 1) { ones++; }
                if (difference == 3) { threes++; }
                previousJoltage = joltage;
            }

            Console.WriteLine($"\n{ones} differences of one and {threes} differences of three"); //debugging

            return ones*threes;
        }

        static void Main(string[] args)
        {
            string[] rawLines = File.ReadAllLines(@"C:\Users\ellio\Documents\programming\c#\vs code\advent of code\inputs\day10.txt");
            Console.WriteLine($"input of length {rawLines.Length} | {string.Join(",",rawLines)}"); //debugging

            //turn puzzle input to a list struct
            List<int> rawList = new List<int>();
            foreach (string ln in rawLines) { rawList.Add(Int32.Parse(ln)); };

            //process puzzle input 
            List<int> sortedAdapters = MergeSort(rawList); //recursive merge sort 

            //add device adapter to the end of the sorted list
            sortedAdapters.Add(sortedAdapters[sortedAdapters.Count-1]+3);
            //"your device has a built-in joltage adapter rated for 3 jolts higher than the highest-rated adapter in your bag"

            Console.WriteLine($"\nprocessed input of length {sortedAdapters.Count} | {string.Join(",", sortedAdapters)}"); //debugging

            //Console.WriteLine(sortedAdapters.Count); debug
            Console.WriteLine($"\npuzzle output | {CountGaps(sortedAdapters)}"); //takes the sorted list 

            Console.ReadKey();
        }
    }
}

using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace _9._1
{

    class Program
    {

        static int CheckNumbers(string[] numbers, int preamble)
        {
            int startIndex, target, sum;
            bool safe = false;

            for(int i = preamble; i < numbers.Length; i++) //target number index (i)
            {
                target = Int32.Parse(numbers[i].Trim());
                startIndex = 1; //start index begins at one to stop it from adding it to itself on initial loop
                Console.Write($"\r>> summing to {target}");
                safe = false;
                for(int j = 0; j < preamble; j++) //j repeats from and for <preamble> numbers before the target to check them against every other number once
                {
                    //k starts at <startIndex> which is incremented after each search, which makes checking each combination more efficient as it only checks each exactly once 
                    //the difference between preambleChoose2 versus preambleSquared operations
                    //Console.WriteLine($"{numbers[j + i - preamble]}");
                    for (int k = startIndex; k < preamble; k++) 
                    {
                        sum = Int32.Parse(numbers[j + i - preamble]) + Int32.Parse(numbers[k + i - preamble]); //finds relative indicies to add
                        if (sum == target) { 
                            safe = true;
                            break;
                        }
                    }
                    if (safe) { break; } //stops searching once the first pair that sum to target is found
                    startIndex++; //reduces search length each time as it does not need to check every number against every other number every single time
                                  //this is because some of the checks have already been performed
                }
                if (!safe) { return target; }
            }

            return -1; //returns if all numbers are legal
        }

        static void Main(string[] args)
        {
            string[] rawLines = File.ReadAllLines(@"C:\Users\ellio\Documents\programming\c#\vs code\advent of code\inputs\day9.txt");
            int preamble = 25;

            int firstOutlier = CheckNumbers(rawLines, preamble);
            Console.WriteLine("\n////////////////////////////////////");
            Console.WriteLine($">> first illegal number: {firstOutlier}");
        }
    }
}

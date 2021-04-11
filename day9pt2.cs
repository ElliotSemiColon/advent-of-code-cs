using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace _9._2
{
    class Program
    {
        public static long target = 144381670;

        static long FindNumber(List<long> input, bool largest) //linear search for largest or smallest number in a list
        {
            long output;

            if (largest)
            {
                output = -200000000000000000; //really small number to search up from
                foreach(long number in input) { if(number > output) { output = number; } }
            }
            else
            {
                output = 200000000000000000; //really big number to search down from
                foreach (long number in input) { if (number < output) { output = number; } }
            }

            return output;
        }

        static long RollingSum(List<long> numbers, List<long> groups , int groupSize) //sums contiguous groups of numbers together
        {
            //newGroups will store all newly summed groups of size groupSize+1, which will be used to compute the sum of those bigger groups more efficiently
            List<long> newGroups = new List<long>(), targetGroup = new List<long>();
            long sum;
            long output;

            Console.WriteLine($"rolling group size {groupSize}");

            for(int i = 0; i <= numbers.Count - groupSize; i++)
            {
                //Console.WriteLine($"summing {groups[i]} + {numbers[i+groupSize-1]}");
                //no nested for loop needed if we sum using the previously summed group of size n-1 and add the next contiguous number 
                //this allows us to efficiently make the larger rolling group
                sum = groups[i] + numbers[i + groupSize - 1]; //sums [already summed group of n numbers] plus one number to bring it to n+1 group size
                newGroups.Add(sum);

                if (sum == target) {
                    
                    for(int j = 0; j < groupSize; j++)
                    {
                        targetGroup.Add(numbers[j + i]);
                    }

                    output = FindNumber(targetGroup, false) + FindNumber(targetGroup, true);
                    //Console.WriteLine($"output {output} from {numbers[i]} + {numbers[i + groupSize - 1]}");
                    return output;
                } //returns the sum of the smallest and largest numbers in the group
            }
            output = RollingSum(numbers, newGroups, groupSize + 1); //recurses with a group size n+1 and new groups of n+1 size

            return output;
        }

        static void Main(string[] args)
        {
            string[] rawLines = File.ReadAllLines(@"C:\Users\ellio\Documents\programming\c#\vs code\advent of code\inputs\day9.txt");

            List<long> groups = new List<long>(); //summed groups 

            foreach(string ln in rawLines)
            {
                groups.Add(Int64.Parse(ln));
                //Console.WriteLine(ln);
            }

            List<long> rawNumbers = groups; //raw numbers from the file

            long output = RollingSum(rawNumbers, groups, 2);

            Console.WriteLine($"output {output}");
        }
    }
}

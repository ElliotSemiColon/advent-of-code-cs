using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace _13._1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] rawLines = File.ReadAllLines(@"B:\Elliot Buckingham\Documents\Visual Studio 2019\repos\advent of code\inputs\13.txt");
            Console.WriteLine($"input of length {rawLines.Length}"); //debugging

            int start = Int32.Parse(rawLines[0]), temp, wait;
            string[] busses = rawLines[1].Split(",");

            int departs = 10000;

            foreach(string bus in busses)
            {
                if(bus != "x")
                {
                    temp = start;
                    while (true)
                    {
                        if (temp % Int32.Parse(bus) == 0) { break; }
                        temp++;
                    }
                    wait = temp - start;

                    if(wait < departs) { 
                        departs = wait;
                        Console.WriteLine(departs * Int32.Parse(bus));
                    }
                }
            }

            //Console.WriteLine(departs);

            Console.ReadKey();
        }
    }
}

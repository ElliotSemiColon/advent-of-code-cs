using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace day_8._1
{
    class Program
    {
        static int FindLoop(string[] ops, int acc)
        {
            Regex opData = new Regex(@"(\w{3})\s([-+])([0-9]+)");
            string[] operation; //opcode, sign and data
            int line = 0;

            //each time an instruction is executed, it will be added to a dictionary. it will have a key of the line number and value of null.
            //if a line is executed more than once the dictionary will throw an exception 
            Dictionary<int, string> executed = new Dictionary<int, string>();
            //its a bit hacky but its faster than searching an array of all executed operations probably 
            try
            {
                while (true) //run until executed.Add(line, null) throws an exception
                {
                    operation = new string[3]{ opData.Match(ops[line]).Groups[1].Value, //returns opcode, sign and data using the three capture cases
                    opData.Match(ops[line]).Groups[2].Value,
                    opData.Match(ops[line]).Groups[3].Value };

                    executed.Add(line, null); //happens before the acc is modified so it outputs accurate value

                    if (operation[0] == "nop") { line++; }
                    else if (operation[0] == "jmp")
                    {
                        if (operation[1] == "+") { line += Int16.Parse(operation[2]); } else { line -= Int16.Parse(operation[2]); }
                    }
                    else { 
                        if (operation[1] == "+") { acc += Int16.Parse(operation[2]); } else { acc -= Int16.Parse(operation[2]); }
                        line++;
                    }
                }
            }
            catch (ArgumentException)
            {
                Console.WriteLine("\nloop detected");
            }

            return acc; //returns value of acc when loop is found 
        }

        static void Main(string[] args)
        {
            string[] rawLines = File.ReadAllLines(@"C:\Users\ellio\Documents\programming\c#\vs code\advent of code\inputs\day8.txt");

            int acc = FindLoop(rawLines, 0);

            Console.WriteLine(acc);

            Console.ReadKey();
        }
    }
}

using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace _8._2
{
    class Program
    {
        static List<int> FindJmpNop(string[] ops) //returns the indices of all jump and noOperation ops
        {
            List<int> lines = new List<int>();

            //finds all nop and jmp operations 
            for(int i = 0; i < ops.Length; i++) { if(GetOperation(ops[i])[0] == "jmp" || GetOperation(ops[i])[0] == "nop") { lines.Add(i); } } 

            return lines;
        }

        static string[] GetOperation(string line) //returns opcode, sign and data using the three capture cases
        {
            Regex opData = new Regex(@"(\w{3})\s([-+])([0-9]+)");
            return new string[3]{ opData.Match(line).Groups[1].Value, opData.Match(line).Groups[2].Value, opData.Match(line).Groups[3].Value };
        }

        static int FindLoop(string[] ops, int acc)
        {
            string[] operation; //opcode, sign and data
            int line;

            List<int> linesJmpNop = FindJmpNop(ops);

            //each time an instruction is executed, it will be added to a dictionary. it will have a key of the line number and value of null.
            //if a line is executed more than once the dictionary will throw an exception 
            Dictionary<int, string> executed;
            //its a bit hacky but its faster than searching an array of all executed operations probably 

            foreach (int ln in linesJmpNop)
            { //tries flipping every single jump and noOperation opcode until the program doesnt loop
                acc = 0;
                line = 0;
                executed = new Dictionary<int, string>();

                try
                {
                    while (true) //run until executed.Add(line, null) throws an exception *
                    {
                        operation = GetOperation(ops[line]);
                       
                        executed.Add(line, null); //happens before the acc is modified so it outputs accurate value

                        if (line == ln) //if line is the line to be flipped this iteration 
                        {
                            if (operation[0] == "nop") { operation[0] = "jmp"; } else if (operation[0] == "jmp") { operation[0] = "nop"; }
                        }

                        //Console.WriteLine($"{operation[0]} {operation[1]}{operation[2]}");

                        if (operation[0] == "nop") { line++; }
                        else if (operation[0] == "jmp")
                        {
                            if (operation[1] == "+") { line += Int16.Parse(operation[2]); } else { line -= Int16.Parse(operation[2]); }
                        }
                        else
                        {
                            if (operation[1] == "+") { acc += Int16.Parse(operation[2]); } else { acc -= Int16.Parse(operation[2]); }
                            line++;
                        }
                        if (line == ops.Length) { break; } // * or until it reaches the line immediately below the last line
                    }
                }
                catch (ArgumentException)
                {
                    Console.Write($"\rloop detected at line {line}");
                }

                if (line == ops.Length)
                {
                    Console.WriteLine("\nprogram terminates correctly");
                    break; 
                } //breaks out of foreach so as to not reset acc again
            }

            return acc; //returns value of acc when loop is found 
        }

        static void Main(string[] args)
        {
            string[] rawLines = File.ReadAllLines(@"C:\Users\ellio\Documents\programming\c#\vs code\advent of code\inputs\day8.txt");

            int acc = FindLoop(rawLines, 0);

            Console.WriteLine($"\n>> acc value {acc}");

            Console.ReadKey();
        }
    }
}

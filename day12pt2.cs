using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace _12._1
{
    class Ship
    {

        private int x = 0;
        private int y = 0;
        private int direction = 1800000; // pointing along positive x axis (360 * 5000) (large to avoid modulo behaviour around 0)
        private string[] instructions;

        public Ship(string[] _instructions)
        {
            instructions = _instructions;
        }

        public int Execute() //returns final coords
        {
            string operation, opcode;
            int operand, manhattan;
            int wx = 10, wy = 1, temp, j; //waypoint coords and iterator for rotation

            for(int i = 0; i < instructions.Length; i++)
            {
                Console.WriteLine($"{wx},{wy}");
                operation = instructions[i];
                opcode = operation.Substring(0, 1);
                operand = Int32.Parse(operation.Substring(1, operation.Length-1));

                switch (opcode)
                {
                    case "N":
                        
                        wy += operand;
                        break;
                    case "S":
                        
                        wy -= operand;
                        break;
                    case "E":
                        
                        wx += operand;
                        break;
                    case "W":
                       
                        wx -= operand;
                        break;
                    case "L":
                        j = operand / 90;
                        
                        for(int k = 0; k < j; k++)
                        {
                            //anticlockwise 90 degrees
                            temp = wy;
                            wy = wx;
                            wx = -temp;
                        }

                        break;
                    case "R":
                        j = operand / 90;

                        for (int k = 0; k < j; k++)
                        {
                            //clockwise 90 degrees
                            temp = wy;
                            wy = -wx;
                            wx = temp;
                        }

                        break;
                    case "F":
                        //to waypoint
                        for (int k = 0; k < operand; k++)
                        {
                            x += wx;
                            y += wy;
                        }

                        break;
                    default:
                        //no recognised opcode
                        break;
                }

                Console.WriteLine(operation);
            }

            manhattan = Math.Abs(x) + Math.Abs(y);

            return manhattan;
        }

    }

    class Program
    {

        static void Main(string[] args)
        {
            string[] rawLines = File.ReadAllLines(@"B:\Elliot Buckingham\Documents\Visual Studio 2019\repos\advent of code\inputs\12.txt");
            Console.WriteLine($"input of length {rawLines.Length}"); //debugging

            Ship ship = new Ship(rawLines);
            Console.WriteLine(ship.Execute());

            Console.WriteLine();

            Console.ReadKey();
        }
    }
}

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
        private int direction = 1800000; // pointing along positive x axis (360 * 5000)
        private string[] instructions;

        public Ship(string[] _instructions)
        {
            instructions = _instructions;
        }

        public int Execute() //returns final coords
        {
            string operation, opcode;
            int operand, manhattan;

            for(int i = 0; i < instructions.Length; i++)
            {
                operation = instructions[i];
                opcode = operation.Substring(0, 1);
                operand = Int32.Parse(operation.Substring(1, operation.Length-1));

                switch (opcode)
                {
                    case "N":
                        //strafe north
                        y += operand;
                        break;
                    case "S":
                        //strafe south
                        y -= operand;
                        break;
                    case "E":
                        //strafe east
                        x += operand;
                        break;
                    case "W":
                        //strafe west
                        x -= operand;
                        break;
                    case "L":
                        //turn left
                        direction -= operand;
                        break;
                    case "R":
                        //turn right
                        direction += operand;
                        break;
                    case "F":
                        //forward
                        
                        switch (direction % 360)
                        {
                            case 0:
                                x += operand;
                                break;
                            case 90:
                                y -= operand;
                                break;
                            case 180:
                                x -= operand;
                                break;
                            case 270:
                                y += operand;
                                break;
                        }

                        break;
                    default:
                        //no recognised opcode
                        break;
                }
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

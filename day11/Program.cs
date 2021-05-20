using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace day11
{
    class Program
    {

        static char Lookup(int x, int y, string[] cells) //returns char at x, y
        {
            string row = ""; char target = Convert.ToChar("L"); 

            if (y >= 0 && y < cells.Length) { row = cells[y]; }
            if (x >= 0 && x < row.Length) { target = Convert.ToChar(row.Substring(x, 1)); }

            return target;
        }

        static int GetNeighbours(int x, int y, string[] cells) //returns number of living neighbours (x and y reference the rawLines array like a grid)
        {
            int total = 0;
            int width = cells[0].Length, height = cells.Length, dx, dy;

            for(int i = 0; i < 9; i++) 
            {
                dx = (i % 3) - 1;
                dy = (i / 3) - 1;

                if(!(dx == 0 && dy == 0)) {

                    char cell = Lookup(x + dx, y + dy, cells);

                    if (cell == Convert.ToChar("L") || cell == Convert.ToChar(".")) { total++; } //counts the number of vacant seats 
                }
            }

            return total;
        }

        static void SeatsToPopulate(string[] cells)
        {
            List<int[]> coords = new List<int[]>();
            int x = 0, y = 0;

            for(int i = 0; i < cells.Length * cells[0].Length; i++)
            {
                x = i % cells[0].Length;
                y = i / cells.Length; //generate all coordinates in the cells grid

                if (GetNeighbours(x, y, cells) == 8) { coords.Add(new int[] { x, y }); } 
            }
            

        }

        static void Main(string[] args)
        {
            string[] rawLines = File.ReadAllLines(@"C:\Users\ellio\Documents\programming\c#\vs\advent of code\inputs\day11test.txt");
            //Console.WriteLine(string.Join(",", rawLines));

            //Lookup(2, 0, rawLines);



            Console.ReadKey();
        }
    }
}

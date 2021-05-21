using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace day11
{
    class Program
    {
        static char Lookup(int x, int y, string[] cells) //returns char at x, y
        {
            string row = ""; char target = Convert.ToChar("L"); //default to L if x and y outside of grid (symbolising the lack of populated seats outside the ferry)

            if (y >= 0 && y < cells.Length) { row = cells[y]; }
            if (x >= 0 && x < row.Length) { target = Convert.ToChar(row.Substring(x, 1)); }

            return target;
        }

        static string[] Set(int x, int y, string[] cells, string input) //sets a char/cell at x, y to state input
        {
            char target = Convert.ToChar(input);

            if (y >= 0 && y < cells.Length && x >= 0 && x < cells[0].Length) { 
                cells[y] = $"{cells[y].Substring(0, x)}{target}{cells[y].Substring(x + 1, cells[y].Length - x - 1)}";
            }

            return cells;
        }

        static int GetNeighbours(int x, int y, string[] cells) //returns number of vacant seats around a cell (x and y reference the rawLines array like a grid)
        {
            int total = 0;
            int dx, dy;

            if (Lookup(x, y, cells) == Convert.ToChar(".")) { total = 5; } //with 5 dead neighbours the state of a cell doesnt change, meaning if the cell is floor (".") it wont ever change
            else
            {
                for (int i = 0; i < 9; i++)
                {
                    dx = (i % 3) - 1; //cycles through all coordinates in a 3 by 3 grid
                    dy = (i / 3) - 1;

                    if (!(dx == 0 && dy == 0))
                    {

                        char cell = Lookup(x + dx, y + dy, cells);

                        if (cell == Convert.ToChar("L") || cell == Convert.ToChar(".")) { total++; } //counts the number of vacant seats 
                    }
                }
            }

            return total;
        }

        static List<int[]> checkCells(string[] cells) //mode set true if updating cell states, set false if counting occupied seats
        {
            List<int[]> coords = new List<int[]>(); // [x, y, state<1 if populated, 0 if not>]
            int x = 0, y = -1, deadNeighbours;

            for(int i = 0; i < cells.Length * cells[0].Length; i++)
            {
                x = i % cells[0].Length;
                if(x == 0) { y++; } //increment y on each loop of x

                deadNeighbours = GetNeighbours(x, y, cells);

                if (deadNeighbours == 8) { coords.Add(new int[] { x, y, 1 }); } //sets seat to occupied 
                if (deadNeighbours < 5) { coords.Add(new int[] { x, y, 0 }); } //sets seat to vacant
            }

            return coords;
        }

        static string[] Update(string[] cells)
        {
            List<int[]> cellsToUpdate = checkCells(cells); //finds all cells that need be updated and their state 

            Console.Write($"\rupdating {cellsToUpdate.Count} cells...");

            foreach(int[] cell in cellsToUpdate) //does it all at once so it doesnt check then update sequentually 
            {
                if(cell[2] == 0) { cells = Set(cell[0], cell[1], cells, "L"); } //set seat/cell at cell x [0], cell y [1] to vacant/dead 
                if(cell[2] == 1) { cells = Set(cell[0], cell[1], cells, "#"); } //set seat/cell at cell x [0], cell y [1] to occupied/living
            }

            return cells;
        }

        static void Main(string[] args)
        {
            string[] rawLines = File.ReadAllLines(@"B:\Elliot Buckingham\Documents\Visual Studio 2019\repos\advent of code\inputs\day11.txt");
            string cellString;
            int occupiedSeats = 0;

            while(rawLines.Length > 0)
            {
                cellString = string.Join("", rawLines); //using a concatenated string as setting an array equal to rawLines passes it byRef, which wont work for comparison between updates
                rawLines = Update(rawLines);

                //Console.WriteLine(string.Join("\n", rawLines));

                if (string.Join("", rawLines) == cellString)
                {
                    //cellString = string.Join("", rawLines); //joins the entire array so it can be checked linearly

                    for (int i = 0; i < cellString.Length; i++) { if (cellString.Substring(i, 1) == "#") { occupiedSeats++; } }

                    Console.WriteLine($"\nsimulation exited with {occupiedSeats} occupied seats"); //outputs puzzle answer

                    rawLines = new string[] { };
                }
            }

            Console.ReadKey();
        }
    }
}
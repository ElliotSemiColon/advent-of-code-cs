using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ConsoleApp6
{
    class Program
    {
        public const string filePath = @"C:\Users\ellio\Documents\programming\c#\vs code\advent of code\inputs\day7test.txt";
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines(filePath);
            List<string> isGold = new List<string>();
            isGold.Add("shiny gold");
            List<string> containment = new List<string>();
            int validCount = 0;
            bool toggle = false;

            foreach (string element in lines)
            {
                Console.WriteLine($"split 0,1 |{element.Split()[0]}|{element.Split()[1]}|");
                toggle = false;

                Console.WriteLine($"substring |{element.Substring(18)}|");

                if (element.Substring(18).Contains("shiny gold"))
                {
                    Console.WriteLine($"bag |{element.Split()[0] + " " + element.Split()[1]}| contains a gold bag");

                    isGold.Add(element.Split()[0] + " " + element.Split()[1]);
                }                 

                Console.WriteLine();
            }

            foreach (string AAA in isGold)
            {
                Console.WriteLine(AAA);
            }
            Console.WriteLine();

            bool hhhh = false;
            foreach (string body in lines)
            {
                //Console.WriteLine(body.Substring(18));
                hhhh = false;
                foreach (string thing in isGold)
                {
                    if (hhhh == false)
                    {
                        if (body.Substring(18).Contains(thing) == true)
                        {
                            Console.WriteLine($"found |{thing}| in |{body[18..]}|, |{body}|");
                            //Console.WriteLine(thing);
                            validCount += 1;
                            hhhh = true;
                        }
                    }
                }
            }

            Console.WriteLine(validCount);
            Console.ReadKey();
        }
    }
}

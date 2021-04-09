using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace day_7._2
{
    class Program
    {

        static string[] AllMatches(string input, Regex rgx, int group)
        {
            List<string> matches = new List<string>();

            Match match = rgx.Match(input);
            while (match.Success)
            {
                //adds the value of the first capture group to the list 
                matches.Add(match.Groups[group].Value);
                match = match.NextMatch();
            }
            string[] output = matches.ToArray();

            return output;
        }

        static Dictionary<string, string> BuildNestedDict(string input, Regex rgx)
        {
            Dictionary<string, string> nestedDict = new Dictionary<string, string>();

            Match match = rgx.Match(input);
            while (match.Success)
            {
                nestedDict.Add(match.Groups[2].Value, match.Groups[1].Value); //adds capture groups to dict
                match = match.NextMatch();
            }

            return nestedDict;
        }

        static void ToDict(Dictionary<string, Dictionary<string, string>> bags, string[] splitByLine)
        {
            Regex findParentBag = new Regex(@"(\w+\s\w+)\s(bags contain)");
            Regex findChildrenBags = new Regex(@"([0-9])\s(\w+\s\w+)");

            foreach (string ln in splitByLine)
            {
                string parentBag = findParentBag.Match(ln).Groups[1].Value;
                Dictionary<string, string> nestedDict = BuildNestedDict(ln, findChildrenBags);

                //allmatches now has a capture group perameter

                //builds a dictionary of parent bags and their children bags which they contain
                bags.Add(parentBag, nestedDict);
            }
        }

        static int SearchBags(string target, int count, Dictionary<string, Dictionary<string, string>> bags)
        {
            try
            {
                //target is name being searched for
                Dictionary<string, string> bagsInside = bags[target]; //returns the dict at the container bag key

                foreach (KeyValuePair<string, string> kvp in bagsInside)
                {
                    Console.WriteLine(count);
                    count += Int16.Parse(kvp.Value);
                    for(int i = 0; i < Int16.Parse(kvp.Value); i++) //starts a new search thread for the number of each bag type (inefficient but its elegant in code)
                    {
                        count = SearchBags(kvp.Key, count, bags); //searches up through every containing bag, incrementing it by the number of bags it holds 
                    }
                }
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine($"key = {target} is not found.");
            }

            return count;
        }


        static void Main(string[] args)
        {
            string rawText = File.ReadAllText(@"C:\Users\ellio\Documents\programming\c#\vs code\advent-of-code-cs-master\my puzzle inputs\day7.txt");
            //Console.WriteLine($"input:\n{rawText}");

            string[] splitByLine = rawText.Split("\n");

            //container bag, (contained bags, number of each contained bag )
            Dictionary<string, Dictionary<string, string>> bags = new Dictionary<string, Dictionary<string, string>>();

            ToDict(bags, splitByLine);

            //data structure debug
            foreach (KeyValuePair<string, Dictionary<string, string>> kvp in bags)
            {
                Console.WriteLine($"{kvp.Key} contains");
                foreach (KeyValuePair<string, string> bagBagNo in kvp.Value)
                {
                    Console.WriteLine($"{bagBagNo.Value} {bagBagNo.Key} bags");
                }
            }

            int count = SearchBags("shiny gold", 0, bags); //

            //start of a recursive call searching through all bags
            //looks for all dictionary entries with a key value of the searched bag, adds the length of the array 

            Console.WriteLine($"\n{count} individual bags are required inside your single shiny gold bag");

            Console.ReadKey();
        }
    }
}

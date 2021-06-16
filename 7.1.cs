using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

//to switch a project to compile, right-click the solution, enter 'properties' and select the project you want to debug as single startup project

/*initial ideas for day 7 algorithm
    > create a recursive function that takes a bag type to search for as well as the number of bags found
    > initially call the function to look for bags that contain a shiney gold bag 
    > if the searched bag is found to not be contained by any other bag, return the number of container bags found and add it to a total
    > else call the function again on the containing bag
    > do this for every type of bag found to contain the searched bag
    > should result in a total returned thats equal to the number of bags that can be chosen to hold at least one shiney gold bag
*/

namespace day_7
{
    class Program
    {

        static string[] AllMatches(string input, Regex rgx)
        {
            List<string> matches = new List<string>();

            Match match = rgx.Match(input);
            while (match.Success)
            {
                //adds the value of the first capture group to the list 
                matches.Add(match.Groups[1].Value); 
                match = match.NextMatch();
            }
            string[] output = matches.ToArray();

            return output;
        }

        static void ToDict(Dictionary<string, string[]> bags, string[] splitByLine)
        {
            Regex findParentBag = new Regex(@"(\w+\s\w+)\s(bags contain)");
            Regex findChildrenBags = new Regex(@"[0-9]\s(\w+\s\w+)");

            foreach (string ln in splitByLine)
            {
                //returns the value of the first capture group of the first match in each line
                string parentBag = findParentBag.Match(ln).Groups[1].Value; //group indexes start at 1 for some reason
                //Console.WriteLine($"group {parentBag}");

                string[] childrenBags = AllMatches(ln, findChildrenBags);
                //builds a dictionary of parent bags and their children bags which they contain
                bags.Add(parentBag, childrenBags);
            }
        }

        static int SearchBags(string name, int count, Dictionary<string, string[]> bags)
        {
            
            foreach(KeyValuePair<string, string[]> kvp in bags)
            {
                foreach(string bag in kvp.Value)
                {
                    //Console.WriteLine($"{bag} == {name}");
                    if(bag == name) 
                    {
                        count++; //increments count when it finds a bag inside another bag, which counts as a way to store it 
                        Console.WriteLine($"found {name} in {kvp.Key}, searching for {kvp.Key} in all bags");
                        //removes the record just found to contain the searched bag, means it only finds unique containers 
                        //this means it does not count two different bags in the final bag as distinct, as theyve already been counted by themselves
                        bags.Remove(kvp.Key);
                        //returns a count thats been incremented for each unique bag above kvp.Key's search level
                        count = SearchBags(kvp.Key, count, bags); 
                    } 
                }
            }
            //Console.WriteLine(count);

            return count;
        }  

        static void Main(string[] args)
        {
            string rawText = File.ReadAllText(@"C:\Users\ellio\Documents\programming\c#\vs code\advent-of-code-cs-master\my puzzle inputs\day7.txt");
            //Console.WriteLine($"input:\n{rawText}");

            string[] splitByLine = rawText.Split("\n");

            Dictionary<string, string[]> bags = new Dictionary<string, string[]>(); //key is containing bags, value is bags contained within key

            ToDict(bags, splitByLine);

            int count = SearchBags("shiny gold", 0, bags);

            //start of a recursive call searching through all bags
            //searched for the ways of storing the shiney gold bag, works for any bag type

            Console.WriteLine(count);

            Console.ReadKey();
        }
    }
}

using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace day_6 //yeah i didnt manage to get advent of code finished last christmas, so im having a go at some of the problems now
{
    class Program
    {
        static List<string> MatchLetters(string input)
        {
            Regex rgx = new Regex(@"\w");
            List<string> matches = new List<string>();

            Match match = rgx.Match(input);
            while (match.Success)
            {
                //Console.WriteLine(match.Value);
                matches.Add(match.Value);
                match = match.NextMatch();
            }
            return matches;
        }

        static List<List<string>> MatchPeopleLetters(string input) //returns a list of people, each person containing a list of characters
        {
            string[] people = input.Split("\n");

            List<List<string>> output = new List<List<string>>();

            foreach(string person in people)
            {
                //split leaves blank 'people' in (newlines), so only add ones that have content
                if (person != "") { output.Add(MatchLetters(person)); } //adds a new list of single letter strings to the list of lists (each nested list is a person)
            }
            return output;
        }

        static void PartOne(List<List<string>> answers)
        {
            int total = 0;

            foreach (List<string> a in answers)
            {
                bool[] alphabet = new bool[26];
                foreach (string letter in a)
                {
                    byte character = (byte)Convert.ToChar(letter);
                    //once bool val is true then its true until the array is reset 
                    alphabet[character - 97] = true; //maps 'a' character to 0 to correspond to the start index of bool array
                }
                for (int i = 0; i < alphabet.Length; i++)
                {
                    if (alphabet[i]) { total++; }
                }
            }

            Console.WriteLine($"{total} total discrete answers");
        }

        static void PartTwo(List<List<List<string>>> answers)
        {
            int total = 0;

            foreach (var group in answers)
            {
                int[] alphabet = new int[26] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                foreach (List<string> person in group)
                { //counts instances of the answer
                    foreach (string letter in person)
                    {
                        byte character = (byte)Convert.ToChar(letter);
                        //once bool val is true then its true until the array is reset 
                        alphabet[character - 97]++; //maps 'a' character to 0 to correspond to the start index of array
                    }
                }
                //Console.WriteLine(group.Count);
                for (int i = 0; i < alphabet.Length; i++)
                {
                    if (alphabet[i] == group.Count) { total++; }
                    //Console.Write(alphabet[i]);
                }
                //Console.WriteLine($"\ntotal {total}");
            }

            Console.WriteLine($"\n{total} total answers given by all people of the same group");
            //foreach(string letter in answers[2])
            //{
            //    Console.WriteLine(letter);
            //}
            //Console.WriteLine(answers[2].Count);
        }

        static void Main(string[] args)
        {
            //count distinct answers per group and total the groups up
            string rawInput = File.ReadAllText(@"C:\Users\ellio\source\repos\advent of code\my puzzle inputs\day6.txt");
            string[] groups = rawInput.Split("\n\r");
            List<List<string>> answers = new List<List<string>>(); //part 1
            List<List<List<string>>> answersPeople = new List<List<List<string>>>(); //fuck me man

            foreach (string group in groups) {
                //Console.WriteLine("group");
                //Console.WriteLine(group);
                answersPeople.Add(MatchPeopleLetters(group)); //add a nested list to the list(groups) of lists(people) of lists(responses)
            }
            //Console.WriteLine(answers.Count);

            //PartOne(answers);
            PartTwo(answersPeople);

            //MatchPeopleLetters(groups[0]);

            Console.ReadKey();
        }
    }
}

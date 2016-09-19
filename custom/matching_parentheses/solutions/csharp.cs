using System;
using System.Collections.Generic;

namespace Solutions
{
    public class Parentheses
    {
        public List<char> Openings { get; private set;}

        public List<char> Closings { get; private set; }

        public Parentheses()
        {
            Openings = new List<char> { '(', '[', '{' };
            Closings = new List<char> { ')', ']', '}' };
        }

        public bool IsOpening(char ch)
        {
            return Openings.Exists(opening => ch == opening);
        }

        public bool IsClosing(char ch)
        {
            return Closings.Exists(closing => ch == closing);
        }

        public char GetMatch(char ch)
        {
            var index = Closings.FindIndex(closing => ch == closing);

            return Openings[index];
        }
    }

    public static class Program
    {
        public static void Main(string[] args)
        {
            var input = GetInput();

            Console.WriteLine(AreAllClosed(input));
        }

        private static string GetInput()
        {
            return Console.ReadLine();
        }

        private static bool AreAllClosed(string input)
        {
            var parentheses = new Parentheses();

            var stack = new List<char>();
            foreach (var parenthesis in input.ToCharArray())
            {
                if (parentheses.IsOpening(parenthesis))
                {
                    stack.Add(parenthesis);
                }
                else if (parentheses.IsClosing(parenthesis))
                {
                    var matching = parentheses.GetMatch(parenthesis);
                    if (stack[stack.Count - 1] != matching)
                    {
                        return false;
                    }
                    else
                    {
                        stack.RemoveAt(stack.Count - 1);
                    }
                }
                else
                {
                    throw new Exception(
                            string.Format("Invalid character {0}", parenthesis));
                }
            }

            return stack.Count == 0;
        }
    }
}

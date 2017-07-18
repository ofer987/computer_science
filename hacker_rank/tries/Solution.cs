using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public static class StringExtensions
{
    public static IEnumerable<string> Substrings(this string str)
    {
        var length = str.Length;
        var i = 1;
        while (i <= length)
        {
            yield return str.Substring(0, i);
            i += 1;
        }
    }
}

public class Contacts : Dictionary<string, bool>
{
    Dictionary<string, int> Partials { get; }

    public Contacts()
    {
        Partials = new Dictionary<string, int>();
    }

    public void Add(string name)
    {
        bool exists;
        foreach (var substring in name.Substrings())
        {
            int count;
            if (Partials.TryGetValue(substring, out count))
            {
                Partials[substring] = count + 1;
            }
            else
            {
                Partials.Add(substring, 1);
            }
        }

        if (!TryGetValue(name, out exists))
        {
            Add(name, true);
        }
    }

    public int Find(string name)
    {
        int count;
        if (Partials.TryGetValue(name, out count))
        {
            return count;
        }
        else
        {
            return 0;
        }
    }
}

public class Solution
{
    public static void Main(String[] args)
    {
        var contacts = new Contacts();
        foreach (var line in ReadLines())
        {
            foreach (var answer in Operation(line, contacts))
            {
                Console.WriteLine(answer.ToString());
            }
        }
    }

    private static IEnumerable<string> ReadLines()
    {
        var count = int.Parse(Console.ReadLine());

        var i = 0;
        while (i < count)
        {
            yield return Console.ReadLine();
            i += 1;
        }
    }

    private static IEnumerable<int> Operation(string line, Contacts contacts)
    {
        var list = line.Split(' ').ToList();
        var oper = list[0];
        var operand = list[1];

        switch (oper)
        {
            case "add":
                contacts.Add(operand);
                break;
            case "find":
                yield return contacts.Find(operand);
                break;
            default:
                break;
        }
    }
}

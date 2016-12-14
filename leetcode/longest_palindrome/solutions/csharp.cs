using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class Extensions
{
    public static string Chain<T>(this IEnumerable<T> list, string middle = "")
    {
        var sb = new StringBuilder();

        sb.Append(list.Take(1).DefaultIfEmpty().FirstOrDefault().ToString());

        foreach(var item in list.Skip(1))
        {
            sb.Append(middle);
            sb.Append(item.ToString());
        }

        return sb.ToString();
    }
}

public class Palindrome
{
    public string Question { get; private set; }

    public Palindrome(string question)
    {
        Question = question;
    }

    public IEnumerable<string> GetEvenPalindromes()
    {
        for (var start = 0; start < Question.Length; start++)
        {
            var length = start + 1;

            var left = Question.Substring(0, length).ToCharArray().Reverse().ToList();
            var right = Question.Substring(length, Question.Length - length).ToCharArray().ToList();

            var palindrome = LongestSimilar(left, right);
            if (palindrome.Length > 1)
            {
                yield return palindrome;
            }
        }
    }

    public IEnumerable<string> GetOddPalindromes()
    {
        for (var start = 1; start < Question.Length - 1; start++)
        {
            var length = start;

            var firstCharacter = Question.Substring(start, 1).First();
            var left = Question.Substring(0, length).ToCharArray().Reverse().ToList();
            var right = Question.Substring(start + 1, Question.Length - length - 1).ToCharArray().ToList();

            var palindrome = LongestSimilar(left, right, firstCharacter);
            if (palindrome.Length > 1)
            {
                yield return palindrome;
            }
        }
    }

    public string Largest()
    {
        var largest = "";
        foreach (var palindrome in GetEvenPalindromes().Concat(GetOddPalindromes()))
        {
            if (palindrome.Length > largest.Length) {
                largest = palindrome;
            }
        }

        return largest;
    }

    private string LongestSimilar(List<char> left, List<char> right, char? firstCharacter = null)
    {
        var i = 0;
        while (true)
        {
            if (i > left.Count-1 || i > right.Count-1)
            {
                break;
            }

            if (left[i] != right[i])
            {
                break;
            }

            i++;
        }

        var sb = new StringBuilder();
        for (var j = i-1; j >= 0; j--)
        {
            sb.Append(left[j]);
        }

        if (firstCharacter.HasValue)
        {
            sb.Append(firstCharacter);
        }

        for (var j = 0; j < i; j++)
        {
            sb.Append(right[j]);
        }

        return sb.ToString();
    }
}

public static class Solution
{
    public static void Main(string[] argv)
    {
        var question = Console.ReadLine();

        var answer = new Palindrome(question).Largest();
        Console.WriteLine(answer);
    }
}

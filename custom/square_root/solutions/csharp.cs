using System;
using System.Collections.Generic;
using System.Linq;

public static class Extensions
{
    private const double Tolerance = 0.5;

    public static double SquareRoot(this int question)
    {
        try
        {
            return SquareRootInternal(question, 0, (double)question);
        }
        catch (Exception exception)
        {
            Console.WriteLine("problem happened {0}", exception.Message);

            return 0.0;
        }
    }

    private static double SquareRootInternal(int question, double start, double end)
    {
        var half = (end + start) / 2.0;
        var square = half * half;

        // Within tolerance
        if (IsWithinTolerance(question, square, Tolerance))
        {
            return half;
        }
        else if (square < question)
        {
            return SquareRootInternal(question, half, end);
        }
        else
        {
            return SquareRootInternal(question, start, half);
        }
    }

    private static bool IsWithinTolerance(int question, double answer, double tolerance)
    {
        return (answer < (question + tolerance)) && (answer > (question - tolerance));
    }
}

public static class Solution
{
    public static void Main(string[] argv)
    {
        var question = GetInteger();
        var answer = question.SquareRoot();
        Console.WriteLine(answer);
    }

    // For integers
    private static int GetInteger()
    {
        return Console.ReadLine().Split(' ').Select(int.Parse).First();
    }
}

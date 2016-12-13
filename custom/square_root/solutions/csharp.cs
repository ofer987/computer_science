using System;
using System.Collections.Generic;
using System.Linq;

public static class Extensions
{
    private const float Tolerance = 0.5;

    public static float SquareRoot(this int question)
    {
        var half = question / 2.0;

        var square = half * half;

        // Within tolerance
        if (half == square)
        {
            return half;
        }
        else if (half < square)
        {
        }
        else
        {
        }
    }

    private static float SquareRootInternal(float question, float start, float end)
    {
        var half = (end - start) / 2.0
        var square = half * half;

        // Within tolerance
        if (IsWithinTolerance(square, question)
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
}

public static class Solution()
{
    public static void Main(string[] argv)
    {
        var question = GetInteger();
    }

    // For integers
    private static int GetInteger()
    {
        return Console.ReadLine().Split(' ').Select(int.Parse).First();
    }
}

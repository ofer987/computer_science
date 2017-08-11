using System;
using System.Collections.Generic;
using System.Linq;

public static class Solution
{
    public static int Main(string[] args)
    {
        int first;
        int second;
        try
        {
            first = int.Parse(Console.ReadLine());
            second = int.Parse(Console.ReadLine());
        }
        catch (Exception exception)
        {
            return -1;
        }

        if (first <= 0 || second <= 0)
        {
            return -1;
        }

        if (first >= second)
        {
            Console.WriteLine(LowestCommonDenominator(first, second));
        }
        else
        {
            Console.WriteLine(LowestCommonDenominator(second, first));
        }

        return 0;
    }

    private static int LowestCommonDenominator(numerator, denominator)
    {
        if (numerator == 0)
        {
            return 0;
        }

        if (denominator == 0)
        {
            throw new DivideByZeroException();
        }

        var multiplier = numerator / denominator;
        var remainder = numerator % denominator;

        if (remainder == 0)
        {
            return denominator;
        }

        return LowestCommonDenominator(denominator, remainder);
    }
}

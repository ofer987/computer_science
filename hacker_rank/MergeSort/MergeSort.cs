namespace MergeSort;

public class MergeSort
{
    public static int[] Sort(int[] values)
    {
        Console.WriteLine($"in Sort(values) with {values.Length} values");
        if (values.Length == 0)
        {
            return new int[0];
        }

        if (values.Length == 1)
        {
            Console.WriteLine("why do I fail here");
            return values;
        }

        if (values.Length == 2)
        {
            if (values[0] == Math.Max(values[0], values[1]))
            {
                var temp = values[0];
                values[0] = values[1];
                values[1] = temp;
            }

            return values;
        }

        int[] firstSubset;
        int[] secondSubset;
        switch (values.Length % 2)
        {
            case 0:
                Console.WriteLine("case 1");
                firstSubset = values
                    .Take(values.Length / 2)
                    .ToArray();

                secondSubset = values
                    .Skip(values.Length / 2)
                    .ToArray();
                Console.WriteLine("case 1 ends");

                return Sort(firstSubset, secondSubset).ToArray();
            case 1:
                Console.WriteLine("case 2");
                firstSubset = values
                    .Take(values.Length + 1 / 2)
                    .ToArray();

                secondSubset = values
                    .Skip(values.Length + 1 / 2)
                    .ToArray();
                Console.WriteLine("case 2 ends");

                return Sort(firstSubset, secondSubset).ToArray();
            default:
                throw new NotImplementedException();
        }
    }

    private static IEnumerable<int> Sort(int[] firstVals, int[] secondVals)
    {
        Console.WriteLine($"in Sort(values: {firstVals.Length}, values: {secondVals.Length})");

        Console.WriteLine("\tFirst Values:");
        foreach (var item in firstVals)
        {
            Console.WriteLine($"\t\t{item}");
        }

        Console.WriteLine("\tSecond Values:");
        foreach (var item in secondVals)
        {
            Console.WriteLine($"\t\t{item}");
        }

        var sortedFirstVals = Sort(firstVals);
        var sortedSecondVals = Sort(secondVals);

        Console.WriteLine("\tSorted First Values:");
        foreach (var item in sortedFirstVals)
        {
            Console.WriteLine($"\t\t{item}");
        }

        Console.WriteLine("\tSorted Second Values:");
        foreach (var item in sortedSecondVals)
        {
            Console.WriteLine($"\t\t{item}");
        }

        var length = sortedFirstVals.Length + sortedSecondVals.Length;
        var results = new List<int>(length);

        var i = 0;
        var j = 0;

        while (i < sortedFirstVals.Length && j < sortedSecondVals.Length)
        {
            var firstVal = sortedFirstVals[i];
            var secondVal = sortedSecondVals[j];

            if (firstVal < secondVal)
            {
                results.Add(firstVal);
                i += 1;
            }
            else
            {
                results.Add(secondVal);
                j += 1;
            }
        }

        while (i < sortedFirstVals.Length)
        {
            results.Add(sortedFirstVals[i]);

            i += 1;
        }

        while (j < sortedSecondVals.Length)
        {
            results.Add(sortedSecondVals[j]);

            j += 1;
        }

        // var finishedIteratingFirstVals = false;
        // var finishedIteratingSecondVals = false;
        // while (!finishedIteratingFirstVals || !finishedIteratingSecondVals)
        // {
        //     var firstVal = sortedFirstVals[i];
        //     var secondVal = sortedSecondVals[j];
        //
        //     // TODO bug
        //
        //     // if (firstVal <= secondVal)
        //     if (finishedIteratingSecondVals || firstVal <= secondVal)
        //     {
        //         Console.WriteLine($"\t{firstVal}");
        //         results.Add(firstVal);
        //         if (i < sortedFirstVals.Length - 1)
        //         {
        //             i += 1;
        //         }
        //     }
        //     // else if (finishedIteratingFirstVals || firstVal > secondVal)
        //     else
        //     {
        //         Console.WriteLine($"\t{secondVal}");
        //         results.Add(secondVal);
        //         if (j < sortedSecondVals.Length - 1)
        //         {
        //             j += 1;
        //         }
        //     }
        //
        //     if (i == sortedFirstVals.Length - 1)
        //     {
        //         finishedIteratingFirstVals = true;
        //     }
        //     if (j == sortedFirstVals.Length - 1)
        //     {
        //         finishedIteratingSecondVals = true;
        //     }
        // }

        return results;
    }
}

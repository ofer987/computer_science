namespace MergeSort;

public class MergeSort
{
    public static int[] Sort(int[] values)
    {
        if (values.Length == 0)
        {
            return new int[0];
        }

        if (values.Length == 1)
        {
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

        int[] sortedFirstVals;
        int[] sortedSecondVals;
        switch (values.Length % 2)
        {
            case 0:
                firstSubset = values
                    .Take(values.Length / 2)
                    .ToArray();

                secondSubset = values
                    .Skip(values.Length / 2)
                    .ToArray();
                break;
            case 1:
                firstSubset = values
                    .Take((values.Length + 1) / 2)
                    .ToArray();

                secondSubset = values
                    .Skip((values.Length + 1) / 2)
                    .ToArray();
                break;
            default:
                throw new NotImplementedException();
        }

        sortedFirstVals = Sort(firstSubset);
        sortedSecondVals = Sort(secondSubset);

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

        return results.ToArray();
    }
}

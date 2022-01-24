namespace MergeSort;

public class Program
{
    public static int Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        var case1 = new int[] { 4, 100, 6, 1, 21, 2, 5, 3, 10, 8, -20 };
        var expected1 = new int[] { -20, 1, 2, 3, 4, 5, 6, 8, 10, 21, 100 };

        var actual = MergeSort.Sort(case1);

        var result = 0;

        if (case1.Length != actual.Length)
        {
            return 1;
        }

        for (var i = 0; i < case1.Length; i += 1)
        {
            var actualItem = actual[i];
            var expectedItem = expected1[i];

            if (actualItem == expectedItem)
            {
                Console.WriteLine($"{actualItem} == {expectedItem}");
            }
            else
            {
                result = 1;
                Console.WriteLine($"{actualItem} != {expectedItem}");
            }
        }

        return result;
    }
}

namespace MergeSort;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        var case1 = new int[] { 4, 100, 6, 1, 2, 5, 3, 10, 8 };
        var expected1 = new int[] { 1, 2, 3, 4, 5, 6, 8, 10, 100 };

        var actual = MergeSort.Sort(case1);

        Console.WriteLine($"actual has {actual.Length} values");
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
                Console.WriteLine($"{actualItem} != {expectedItem}");
            }
        }
    }
}

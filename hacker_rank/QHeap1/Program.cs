global using System.Text.RegularExpressions;

namespace QHeap1;

public enum OperationTypes { Add = 1, Delete, PrintMininimum }

public class Program
{
    public static void Main(string[] args)
    {
        var operationCount = Convert.ToInt32(Console.ReadLine());
        var heap = new MinHeap();

        foreach (var (operation, operand) in  GetOperations(operationCount))
        {
            // Console.WriteLine("I am here");
            switch (operation)
            {
                case OperationTypes.Add:
                    heap.Add(operand);
                    break;
                case OperationTypes.Delete:
                    heap.Delete(operand);
                    break;
                case OperationTypes.PrintMininimum:
                    Console.WriteLine(heap.Minimum);
                    break;
            }
        }


        // int count = (int)questionCount;
        // Console.WriteLine($"char: {questionCount}");
        // Console.WriteLine($"Convert to Int32: {Convert.ToInt32(questionCount)}");
        // Console.WriteLine($"Cast to Int32: {count}");
        //
        // var val = Console.ReadLine();
        //
        // Console.WriteLine($"string: {val}");
        // Console.WriteLine($"string length: {val.Length}");
        // Console.WriteLine($"Convert to Int32: {Convert.ToInt32(val)}");
    }

    private static IEnumerable<(OperationTypes, int)> GetOperations(int questionCount)
    {
        var addDeleteFormat = new Regex(@"(\d+) (\d+)");
        var printFormat = new Regex(@"(\d)");

        for (var i = 0; i < questionCount; i += 1)
        {
            var val = Console.ReadLine();
            if (addDeleteFormat.IsMatch(val))
            {
                var groups = addDeleteFormat.Match(val).Groups;
                var operation = (OperationTypes)Convert.ToInt32(groups[1].Value);
                var operand = Convert.ToInt32(groups[2].Value);

                yield return (operation, operand);
            }
            else if (printFormat.IsMatch(val))
            {
                var groups = printFormat.Match(val).Groups;
                var operation = (OperationTypes)Convert.ToInt32(groups[1].Value);

                yield return (operation, -1);
            }
        }
    }
}

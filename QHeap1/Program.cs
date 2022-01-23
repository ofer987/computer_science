using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace QHeap1
{
    public enum OperationTypes { Add = 0, Delete, PrintMininimum }

    public class Solution
    {
        public static void Main(string[] args)
        {
            var operationCount = Convert.ToInt32(Console.ReadLine());
            var operations = GetOperations(operationCount);

            var heap = new List<int>();
            foreach (var item in operations)
            {
                var (operation, operand) = item;
                switch (operation)
                {
                    case OperationTypes.Add:
                        Add(operand, heap);
                        break;
                    case OperationTypes.Delete:
                        Delete(operand, heap);
                        break;
                    case OperationTypes.PrintMininimum:
                        PrintMininimum(heap);
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

        private static void Add(int operand, IList<int> heap)
        {
        }

        private static void Delete(int operand, IList<int> heap)
        {
        }

        private static void PrintMininimum(IList<int> heap)
        {
        }

        private static IEnumerable<(OperationTypes, int)> GetOperations(int questionCount)
        {
            var format = new Regex(@"(\d+) (\d+)");

            for (var i = 0; i < questionCount; i += 1)
            {
                var val = Console.ReadLine();
                if (format.IsMatch(val))
                {
                    var groups = format.Match(val).Groups;
                    var operation = (OperationTypes)Convert.ToInt32(groups[1].Value);
                    var operand = Convert.ToInt32(groups[2].Value);

                    yield return (operation, operand);
                }
            }
        }
    }
}

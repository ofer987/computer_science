using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class ListExtensions
{
    public static string Join<T>(this List<T> list, string join)
    {
        var sb = new StringBuilder();

        for (var i = 0; i < list.Count; i++)
        {
            sb.Append(i.ToString());

            if (i < list.Count - 1)
            {
                sb.Append(join);
            }
        }

        return sb.ToString();
    }
}

public class Int32
{
    public string ToParentheses()
    {
        var sb = new StringBuilder();

        for (var i = 1; i <= this; i++)
        {
            sb.Append("(");
        }
        for (var i = 1; i <= this; i++)
        {
            sb.Append(")");
        }

        return sb.ToString();
    }
}

public class Group : List<int>
{
    public int Total()
    {
        var result = 0;
        foreach(var i in this)
        {
            result += i;
        }

        return result;
    }

    public string ToString()
    {
        return Join(" ")
    }
}

public static class Solution
{
    public static int Main(string[] args)
    {
        int count;
        try
        {
            count = int.Parse(Console.ReadLine());
        }
        catch(Exception)
        {
            return -1;
        }

        if (count <= 0)
        {
            return -1;
        }

        foreach (var group in GenerateGroups(count))
        {
            Console.WriteLine(group);
        }

        return 0;
    }

    private static IEnumerable<Group> GenerateGroups(Group group, int end)
    {
        for (var i = 1; i <= end; i++)
        {
            if group.Total() + i > end
            {
                yield return group;
            }

            var groupCopy = new Group(group);
            return GenerateGroups(groupCopy, end);
        }
    }
}

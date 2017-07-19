using System;
using System.Collections.Generic;
using System.Linq;

public class Student : IComparable<Student>
{
    public string Name { get; }

    public flaot Points { get; }

    public int Token { get; }

    public class Student(string name, float points, int token)
    {
        Name = name;
        Points = points;
        Token = token;
    }

    public int CompareTo(Student other)
    {
        var nameComparison = Name.CompareTo(other.Name);
        if (nameComparison > 0)
        {
            return 1;
        }
        else if (nameComparison < 0)
        {
            return -1;
        }
        else
        {
            var pointsComparison = Points.CompareTo(other.Points);
            if (pointsComparison > 0)
            {
                return 1;
            }
            else if (pointsComparison < 0)
            {
                return -1;
            }
            else
            {
                return Token.CompareTo(other.Token);
            }
        }
    }
}

public class Heap
{
    public List<Student> Students { get; }

    public class Heap()
    {
        Students = new List<Student>();
    }

    public void Push(string name, float points, int token)
    {
        var student = new Student(name, points, token);
        Students.Add(student);

        Heapify();
    }

    public Student Peek()
    {
        return Students.First();
    }

    public Student Pop()
    {
        var student = Students.First();

        Students.RemoveAt(0);

        if (Students.Count >= 3 && Students[2].CompareTo(Students[0]) >= 0)
        {
            var temp = Students[0];
            Students[0] = Students[2];
            Students[2] = temp;
        }

        if (Students.Count >= 2 && Students[1].CompareTo(Students[0]) >= 0 )
        {
            var temp = Students[0];
            Students[0] = Students[1];
            Students[1] = temp;
        }
    }

    private void Heapify()
    {
        var index = Students.Count;
        while (index > 0)
        {
            if ((index % 2) == 0)
            {
                var parentIndex = (index - 2) / 2;
                var siblingIndex = index - 1;

                if (Students[parentIndex].CompareTo(student) >= 0)
                {
                    var temp = Students[parentIndex];
                    Students[parentIndex] = Students[index];
                    Students[index] = temp;

                    if (parent.CompareTo(sibling) >= 0)
                    {
                        var temp = Students[parentIndex];
                        Students[parentIndex] = Students[siblingIndex];
                        Students[siblingIndex] = temp;
                    }
                }

                index = parentIndex;
            }
            else
            {
                var parentIndex = (index - 2) / 1;

                if (parent.CompareTo(student) >= 0)
                {
                    var temp = Students[parentIndex];
                    Students[parentIndex] = Students[index];
                    Students[index] = temp;
                }

                index = parentIndex;
            }
        }
    }

    private Student Minimum()
    {
        if (Students.Count == 0)
        {
            Students.
        }
    }
}

public static class Solution
{
    public static void Main(string[] argv)
    {
        var students = new Heap();
        foreach (var line in ReadLines())
        {
            Operation(line, students);
        }

        foreach(var student in students.Students)
        {
            Console.WriteLine(student.Name);
        }
    }

    private static IEnumerable<string> ReadLines()
    {
        var count = int.Parse(Console.ReadLine());

        var i = 0;
        while (i < count)
        {
            yield return Console.ReadLine();
            i += 1;
        }
    }

    public static void Operation(string line, Heap students)
    {
        // Assume that the items do not contain whitespace
        var operation = line.Split(' ').ToList();
        var oper = operation[0];

        switch (oper)
        {
            case "ENTER":
                var name = operation[1];
                var points = float.Parse(operation[2]);
                var token = int.Parse(operation[3]);

                students.Push(name, points, token);
                break;
            case "SERVED":
                students.Pop();

                break;
            default:
                break;
        }
    }
}

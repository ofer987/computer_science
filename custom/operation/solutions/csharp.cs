using System;
using System.Collections.Generic;
using System.Linq;

public enum Symbols
{
    Add = 0,
    Subtract,
    Multiplication,
    Division
}

public abstract class Node
{
    public virtual Node Left { get; set; }

    public virtual Node Right { get; set; }

    public abstract int Result();

    public abstract string Value();

    public override string ToString()
    {
        return $"{Left.ToString()} {Right.ToString()} {Value()}";
    }
}

// Leaf
public class Operand : Node
{
    public override Node Left
    {
        get
        {
            throw new Exception("Operands do not have any any children");
        }
    }

    public override Node Right
    {
        get
        {
            throw new Exception("Operands do not have any any children");
        }
    }

    public int Digits { get; private set; }

    public Operand(int digits)
    {
        Digits = digits;
    }

    public override string Value()
    {
        return Digits.ToString();
    }

    public override int Result()
    {
        return Digits;
    }

    public override string ToString()
    {
        return Value();
    }
}

public class Operator : Node
{
    public Symbols Symbol { get; private set; }

    public Operator(Symbols symbol)
    {
        Symbol = symbol;
    }

    public override string Value()
    {
        switch (Symbol)
        {
            case Symbols.Add:
                return "+";
            case Symbols.Subtract:
                return "-";
            case Symbols.Multiplication:
                return "*";
            case Symbols.Division:
                return "/";
            default:
                throw new Exception($"Undefined symbol {Symbol}");
        }
    }

    public override int Result()
    {
        switch (Symbol)
        {
            case Symbols.Add:
                return Left.Result() + Right.Result();
            case Symbols.Subtract:
                return Left.Result() - Right.Result();
            case Symbols.Multiplication:
                return Left.Result() * Right.Result();
            case Symbols.Division:
                return Left.Result() / Right.Result();
            default:
                throw new Exception($"Undefined symbol {Symbol}");
        }
    }
}

public class Operation
{
    public Node Root { get; private set; }

    public Operation(string str)
    {
        Root = Parse(str);
    }

    public int Result()
    {
        return Root.Result();
    }

    public override string ToString()
    {
        return Root.ToString();
    }

    private Node Parse(string str)
    {
        var values = str.Split(' ');

        var queue = new Queue<Node>();
        foreach (string val in values)
        {
            Node node = null;
            if (val == "+")
            {
                node = new Operator(Symbols.Add);
            }
            else if (val == "-")
            {
                node = new Operator(Symbols.Subtract);
            }
            else if (val == "*")
            {
                node = new Operator(Symbols.Multiplication);
            }
            else if (val == "/")
            {
                node = new Operator(Symbols.Division);
            }
            else
            {
                node = new Operand(int.Parse(val));
            }

            queue.Enqueue(node);

            if (queue.Count == 3)
            {
                var left = queue.Dequeue();
                var oper = queue.Dequeue();
                var right = queue.Dequeue();

                oper.Left = left;
                oper.Right = right;

                queue.Enqueue(oper);
            }
        }

        return queue.Dequeue();
    }
}

public class Solution
{
    public static void Main(string[] argv)
    {
        foreach (var operationString in ReadOperations())
        {
            var operation = new Operation(operationString);

            Console.WriteLine(operation.ToString());
            Console.WriteLine(operation.Result());
        }
    }

    private static IEnumerable<string> ReadOperations()
    {
        while (true)
        {
            var operation = Console.ReadLine();
            if (operation == null)
            {
                break;
            }

            yield return operation;
        }
    }
}

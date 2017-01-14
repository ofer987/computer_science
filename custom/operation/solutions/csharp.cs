using System;
using System.Collections.Generic;
using System.Linq;

public enum Symbols
{
    None = 0,
    Addition,
    Subtraction,
    Multiplication,
    Division
}

public class Precendence : Comparer<Symbol>
{
    public Symbols Symbol { get; private set; }

    public OperatorSymbol(Symbols symbol)
    {
        Symbol = symbol;
    }

    public override int Compare(Symbol x, Symbol y)
    {
        if (x == Symbol.Addition || x == Symbol.Subtraction)
        {
            if (y == Symbol.Addition || y == Symbol.Subtraction)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
        else
        {
            if (y == Symbol.Addition || y == Symbol.Subtraction)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }

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
            case Symbols.Addition:
                return "+";
            case Symbols.Subtraction:
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
            case Symbols.Addition:
                return Left.Result() + Right.Result();
            case Symbols.Subtraction:
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

        var previousSymbol = Symbols.None;
        var queue = new Queue<Node>();
        foreach (var val in values)
        {
            Node node = null;
            if (val == "+")
            {
                node = new Operator(Symbols.Addition);
            }
            else if (val == "-")
            {
                node = new Operator(Symbols.Subtraction);
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

                if (new Precendence.Compare(oper.Symbol, previousSymbol == 1))
                {
                    oper.Left = left;
                    oper.Right = right;
                }
                else
                {
                    queue.Enqueue(oper);
                }

                var previousSymbol = oper.Symbol;
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

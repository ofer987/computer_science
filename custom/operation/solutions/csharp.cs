public enum Symbols
{
    Add = 0;
    Subtract;
    Multiplication;
    Division
}

public abstract class Node
{
    public abstract string Value();

    public string ToString()
    {
        return $"{Left Right Value}"
    }
}

// Leaf
public class Operand : Node
{
    public int Digits { get; private set; }

    public Operand(int digits)
    {
        Digits = digits;
    }

    public override string Value()
    {
        return Digits.ToString();
    }

    public int Result()
    {
        return Digits;
    }
}

public class Operator : Node
{
    public Symbols Symbol { get; private set; }

    public Node Left { get; private set; }

    public Node Right { get; private set; }

    public Operator(Symbols symbol, Node left, Node right)
    {
        Symbol = symbol;
        Left = left;
        Right = right;
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
                return "/"
            default:
                throw new Exception($"Undefined symbol {Symbol}");
        }
    }

    public int Result()
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

    private Node Parse(string str)
    {
        var values = str.Split(' ');

        foreach (string val in values)
        {
            if (val == "+")
            {
                new Operator(Symbols.Add);
            }
            else if (val == "+")
            {
                new Operator(Symbols.Subtract);
            }
            else if (val == "+")
            {
                new Operator(Symbols.Multiplication);
            }
            else if (val == "+")
            {
                new Operator(Symbols.Division);
            }
            else
            {
                new Operand(int.Parse(val));
            }
        }
    }
}

public class Solution
{
    public static void Main(string[] argv)
    {
        operations
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

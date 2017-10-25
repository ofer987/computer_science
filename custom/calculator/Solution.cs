using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Operand
{
  public int Value { get; private set; }

  public Operand(int val)
  {
    Value = val;
  }
}

public class Operator
{
  public enum Values { Plus = 0, Minus, Multiplication, Division }

  public Values Value { get; private set; }

  public bool IsValid
  {
    get
    {
      return Value != None;
    }
  }

  public Operator(char val)
  {
    switch (val)
    {
      case '+':
        Value = Plus;
        break;
      case '-':
        Value = Minus;
        break;
      case '*':
        Value = Multiplication;
        break;
      case '/':
        Value = Division;
        break;
      default:
        throw new Exception("The Operator ({0}) does not exist", val)
        break;
    }
  }
}

public class Operation
{
  public Operator Oper { get; private set; }

  public Operand Left { get; private set; }

  public Operand Right { get; private set; }

  public class Operation(Operator oper, Operand left, Operand right)
  {
    Oper = oper;
    Left = left;
    Right = right;
  }

  public Operand Result()
  {
    switch (Oper)
    {
      case Plus:
        return new Left.Value + Right.Value;
      case Minus:
        return new Left.Value - Right.Value;
      case Multiplication:
        return new Left.Value * Right.Value;
      case Division:
        return new Left.Value / Right.Value;
    }
  }
}

public class Calculator
{
  public string Expression { get; private set; }

  public Queue<Operation> Pluses { get; private set; }

  public Queue<Operation> Minuses { get; private set; }

  public Queue<Operation> Multiplications { get; private set; }

  public Queue<Operation> Divisions { get; private set; }

  public Calculator(string expression)
  {
    Expression = expression;
  }

  private bool Parse()
  {
    foreach (var ch in Expression)
    {
      switch (ch)
      {
        case ' ':
          break;
        case ''
      }
    }
  }
}

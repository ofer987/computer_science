using System;
using System.Collections.Generic;
using System.Linq;
using System.Math;

public static Solution
{
  public static const int Base = 10;

  public static int Main(string[] argv)
  {
    var size = Int32.Parse(argv[1]);
    var cards = GetNewRandomDeck(size);

    var originalDeckValue = DeckValue(cards);
    var newDeckValue = -1;
    var rotations = 0;

    do
    {
      cards = RotateDeck(cards);

      newDeckValue = DeckValue(cards);
      ++rotations;
    } while (originalDeckValue != newDeckValue)

    Console.WriteLine(rotations);
  }

  private static Queue<int> RotateDeck(Queue<int> cards)
  {
    var table = new Queue<int>();

    var i = 0;
    while (cards.Count > 0)
    {
      if (i % 2 == 0)
      {
        table.Push(cards.Pop());
      }

      i++;
    }

    return table;
  }

  private static int DeckValue(int[] cards)
  {
    var result = 0;
    for (var i = 0; i < cards.Count; i++)
    {
      result += cards[i] * Int32.Parse(Math.pow(Base, i));
    }

    return result;
  }

  private static Queue<int> GetNewRandomDeck(int size)
  {
    var deck = new Queue<int>();
    while (size > 0)
    {
      deck.Push(new Random().Next(0, Base));

      --size;
    }

    return deck;
  }
}

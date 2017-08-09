using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Word
{
    public string Value { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public Word(string val, DateTime createdAt)
    {
        Value = val;
        CreatedAt = createdAt;
    }

    public static IWordCount Sort(IEnumerable<Word> words, DateTime since)
    {
        var wordCountDictionary = new Dictionary<string, int>();
        IWordCount head = new NullWordCount();
        IWordCount tail = new NullWordCount();

        foreach (var word in words)
        {
            if (word.CreatedAt < since)
            {
                continue;
            }

            IWordCount node;
            if (wordCountDictionary.TryGetValue(word.Value, out node))
            {
                var right = node.Right;
                var left = node.Left;
                var leftLeft = left.Left;

                node.Increment();
                if (node.Count > left)
                {
                    leftLeft.Right = node;
                    node.Left = leftLeft;

                    node.Right = left;
                    left.Left = node;

                    left.Right = right;
                    right.Left = left;

                    if (head == left)
                    {
                        head = node;
                    }
                }
            }
            else
            {
                var temp = tail;
                tail = new WordCount(word.Value);
                tail.Left = temp;
                temp.Right = tail;
                wordCountDictionary.Add(word.Value, tail);

                if (head == null)
                {
                    head = tail;
                }
            }
        }

        return head;
    }

    public IEnumerable<string> TopWords(IWordCount head)
    {
        if (head != null)
        {
            yield return head.Word;
        }

        IWordCount node = head;
        while (!(node = node.Right).IsNull())
        {
            yield return node.Word;
        }
    }
}

public interface IWordCount
{
    IWordCount Left { get; set; }

    IWordCount Right { get; set; }

    string Word { get; }

    string Count { get; }

    void Increment();

    string ToString();

    bool IsNull();
}

public class WordCount : IWordCount
{
    public IWordCount Left { get; set; }

    public IWordCount Right { get; set; }

    public string Word { get; private set; }

    public int Count { get; private set; }

    public WordCount(string word)
    {
        Word = word;
        Count = 1;
        Left = new NullWordCount();
        Right = new NullWordCount();
    }

    public void Increment()
    {
        Word++;
    }

    public string ToString()
    {
        return Count.ToString();
    }

    public bool IsNull()
    {
        return false;
    }
}

public class NullWordCount : IWordCount
{
    public IWordCount Left { get { return new NullWordCount(); } set {} }

    public IWordCount Right { get { return new NullWordCount(); } set {} }

    public string Word { get { return null; } }

    public int Count { get { return 0; } }

    public void Increment() {}

    public string ToString()
    {
        return null;
    }

    public bool IsNull()
    {
        return true;
    }
}

public class Solution
{
    public static void Main(string[] args)
    {
        var since = new DateTime(Console.ReadLine());
        var words = ReadWords();

        var head = Word.Sort(words, since);
        foreach (var word in Word.TopWords(head))
        {
            Console.WriteLine(word);
        }
    }

    private static IEnumerable<Word> ReadWords()
    {
        string line;
        while ((line = Console.ReadLine()) != null)
        {
            yield return new Word(line, DateTime.Now());
        }
    }
}


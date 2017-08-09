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

    public static IEnumerable<Word> TopWords(IEnumerable<Word> words, DateTime since)
    {
        var recentWords = words
            .Where(word => word.CreatedAt >= since)
            .Select(word => word.Value);

        var wordCountDictionary = new Dictionary<string, int>();
        foreach (var word in recentWords)
        {
            int count;
            if (wordCountDictionary.TryGetValue(word.Value, out count))
            {
                wordCountDictionary[word.Value] = count + 1;
            }
            else
            {
                wordCountDictionary.Add(word, 1);
            }
        }

        return wordCountDictionary
            .OrderByDescending(pair => pair.Value)
            .Select(pair => pair.Key);
    }
}

public class Solution
{
    public static void Main(string[] args)
    {
        var words = ReadWords();

        foreach (var wordValue in Word.TopWords(words, since))
        {
            Console.WriteLine(wordValue);
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


using System;
using System.Linq;
using System.Collections.Generic;

public class Solution
{
  public static void Main(string[] argv)
  {
    List<List<string>> groups;

    var booleans = ToBooleans(groups);

    var movies = Associations(groups, booleans);

    var mostAssociatedMovie = movies.
      OrderByDescending(pair => pair.Item2).
      First().
      Select(pair => pair.Item1);
  }

  private static IEnumerable<int> ToBooleans(IEnumerable<IEnumerable<string>> groups)
  {
    var frequencies = new Dictionary<string, int>();
    foreach (var group in groups)
    {
      var counter = 0;
      bool exists = false;
      foreach (var movie in group)
      if frequencies.TryGetValue(out exists)
      {
      }
      else
      {
        frequencies[movie] = counter;
        counter += 1;
      }
    }

    foreach (var group in groups)
    {
      var bits = 0;
      foreach (var movie in group)
      {
        var index = frequencies[movie];

        bits += Convert.ToInt32(Math.pow(2, index));
      }

      yield return bits;
    }
  }

  private static Dictionary<string, int> Associations(IEnumerable<IList<string>> groups, IList<int> booleans)
  {
    var movies = new Dictionary<string, int>();

    for (var i = 0; i < groups.Count; i++)
    {
      foreach (var movie in groups[i])
      {
        var boolean = booleans[i];

        int existingBoolean;
        if (movies.TryGetValue(movie, out existingBoolean))
        {
          movies[movie] = existingBoolean & boolean;
        }
        else
        {
          movies.Add(movie, boolean);
        }
      }
    }

    return movies;
  }

  private static Tuple<string, int> Count(IDictionary<string, int> movies)
  {
    foreach (var pair in movies)
    {
      var count = Convert.
        ToString(pair.Value, 2).
        ToCharArray().
        Select(ch => ch == '1').
        Count();

      yield return new Tuple<pair.Key, count);
    }
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using KnapSack.Extensions;

namespace KnapSack
{
    public class NoCoins : ICoins
    {
        public SpendingTypes Spending { get { return SpendingTypes.Misspent; } }

        public bool IsSuccess { get { return false; } }

        public IEnumerable<int> Coins { get { return Enumerable.Empty<int>(); } }

        public int Sum { get { return 0; } }

        public int Count { get { return 0; } }

        public override string ToString()
        {
            return Sum.ToString();
        }
    }
}

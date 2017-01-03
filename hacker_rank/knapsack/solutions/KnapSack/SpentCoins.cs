using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using KnapSack.Extensions;

namespace KnapSack
{
    public class SpentCoins : ICoins
    {
        public static int Compare(ICoins first, ICoins second)
        {
            if (first.Sum > second.Sum)
            {
                return 1;
            }
            else if (first.Sum < second.Sum)
            {
                return -1;
            }

            if (first.Count < second.Count)
            {
                return 1;
            }
            else if (first.Count > second.Count)
            {
                return -1;
            }

            return 0;
        }

        public SpendingTypes Spending { get; private set; }

        public bool IsSuccess
        {
            get
            {
                return Spending == SpendingTypes.All ||
                    Spending == SpendingTypes.Almost;
            }
        }

        public IEnumerable<int> Coins { get; private set; }

        public int Sum { get; private set; }

        public int Count { get; private set; }

        public SpentCoins(SpendingTypes spending, IEnumerable<int> coins)
        {
            Spending = spending;
            Coins = coins;
            Sum = GetSum(coins);
            Count = Coins.Count();
        }

        public override string ToString()
        {
            return Sum.ToString();
        }

        private int GetSum(IEnumerable<int> coins)
        {
            try
            {
                return Coins.Aggregate((sum, coin) => sum += coin);
            }
            catch (InvalidOperationException)
            {
                return 0;
            }
        }
    }
}

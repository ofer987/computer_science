using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using KnapSack.Extensions;

namespace KnapSack
{
    public struct SpentCoins
    {
        public bool IsSuccess { get; private set; }

        public IEnumerable<int> Coins { get; private set; }

        private int? sum;
        public int Sum
        {
            get
            {
                if (sum == null)
                {
                    sum = 0;
                    sum = Coins.Aggregate((sum, coin) => sum += coin);
                }

                return sum.Value;
            }
        }

        public SpentCoins(bool isSuccess, IEnumerable<int> coins)
        {
            IsSuccess = isSuccess;
            Coins = coins;
            sum = null;
        }
    }
}

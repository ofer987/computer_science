using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using KnapSack.Extensions;

namespace KnapSack
{
    public interface ICoins
    {
        bool IsSuccess { get; }

        IEnumerable<int> Coins { get; }

        int Sum { get; }

        int Count { get; }
    }
}

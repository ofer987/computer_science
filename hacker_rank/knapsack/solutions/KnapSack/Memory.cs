using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnapSack
{
    public class Memory
    {
        private Dictionary<int, List<int>> Storage = new Dictionary<int, List<int>>();

        public List<int> this[int key]
        {
            get
            {
                List<int> val;
                if (Storage.TryGetValue(key, out val))
                {
                    return val;
                }

                return null;
            }

            set
            {
                var bestResult = this[key];
                Storage[key] =
                    (bestResult != null && bestResult.Count < value.Count) ?
                    bestResult :
                    value;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vexe.Runtime.Types;

namespace WorldsApart
{
    public class CounterRegistrator : BetterBehaviour
    {
        public Dictionary<string, int> counters;

        void Start()
        {
            foreach (var key in counters.Keys)
                CounterManager.Instance().RegisterCounter(key, counters[key]);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldsApart
{
    public class CounterManager
    {
        private static CounterManager _instance;
        private Dictionary<string, Counter> counters;

        public CounterManager()
        {
            counters = new Dictionary<string, Counter>();
        }

        public static CounterManager Instance()
        {
            if(_instance == null) _instance = new CounterManager();
            return _instance;
        }

        public void RegisterCounter(string name, int amount)
        {
            counters.Add(name, new Counter());
            counters[name].count = amount;
        }

        public Counter GetCounter(string name)
        {
            if (counters.ContainsKey(name))
                return counters[name];
            return null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldsApart.Handlers
{
    public class IncrementCounterHandler : Handler
    {
        public string counterName;
        public int amount;
        
        private Counter counter;

        public override void innerDelegate()
        {
            if (counter == null)
                counter = CounterManager.Instance().GetCounter(counterName);
            counter.Increment(amount);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldsApart.Handlers
{
    public class IncrementCounterHandler : Handler
    {
        public Counter counter;
        public int amount;

        public override void invoke()
        {
            if(counter != null)
                counter.Increment(amount);
        }
    }
}

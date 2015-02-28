using WorldsApart.Conditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldsApart.Handlers
{
    public class ConditionalHandler : Handler
    {
        public Handler handler;
        public Condition condition;

        public override void innerDelegate()
        {
            if (condition.isSatisfied())
            {
                handler.innerDelegate();
            }
        }
    }
}

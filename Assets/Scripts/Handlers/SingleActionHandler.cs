using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldsApart.Handlers
{
    public abstract class SingleActionHandler : Handler
    {
        private bool enabled = true;

        public override void invoke()
        {
            if (enabled == true)
            {
                innerDelegate();
                enabled = false;
            }
        }

        public abstract void innerDelegate();
    }
}

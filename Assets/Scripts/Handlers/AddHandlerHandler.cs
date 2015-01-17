using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldsApart.Clickables;

namespace WorldsApart.Handlers
{
    public class AddHandlerHandler : Handler
    {
        public Handler handler;
        public Clickable target;

        public override void innerDelegate()
        {
            target.AddHandler(handler);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldsApart.Clickables;

namespace WorldsApart.Handlers
{
    public class AddHandler_SingleActionHandler : SingleActionHandler
    {
        public Handler handler;
        public Clickable target;

        public override void innerDelegate()
        {
            target.AddHandler(handler);
        }
    }
}

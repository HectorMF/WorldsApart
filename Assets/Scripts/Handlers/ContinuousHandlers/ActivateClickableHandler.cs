using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using WorldsApart.Clickables;

namespace WorldsApart.Handlers
{
    public class ActivateClickableHandler : Handler
    {
        public Clickable clickable;

        public override void invoke()
        {
            clickable.active = true;
        }
    }
}

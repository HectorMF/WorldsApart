using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Vexe.Runtime.Types;

namespace WorldsApart
{
    [Serializable]
    public abstract class Handler
    {
        internal GameObject gameObject;

        public abstract void invoke();
    }
}

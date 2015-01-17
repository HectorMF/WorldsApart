using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Vexe.Runtime.Types;

namespace WorldsApart.Handlers
{
    [Serializable]
    public enum HandlerType
    {
        Normal,
        SingleAction
    }

    [Serializable]
    public abstract class Handler
    {
        internal GameObject gameObject;
        public HandlerType type = HandlerType.Normal;
        [Hide]
        public bool enabled = true;

        public void Invoke()
        {
            if (type == HandlerType.Normal || enabled == true)
            {
                innerDelegate();
                enabled = false;
            }
        }

        public abstract void innerDelegate();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldsApart.Scripting
{
    [Serializable]
    public abstract class Script
    {
        public abstract void Start(Action OnFinish);
    }
}

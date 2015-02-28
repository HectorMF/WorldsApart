using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldsApart.Conditions
{
    [Serializable]
    public abstract class Condition
    {
        public abstract bool isSatisfied();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Utility
{
    public class Tuple<T1, T2>
    {
        public Tuple(T1 firstItem, T2 secondItem)
        {
            item1 = firstItem;
            item2 = secondItem;
        }

        public T1 item1;
        public T2 item2;
    }
}

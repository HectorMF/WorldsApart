using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WorldsApart
{
    public class Counter
    {
        public int count;
        public string name;

        public void Increment(int amount)
        {
            count += amount;
            UnityEngine.Debug.Log(count);
        }

        public void Decrement(int amount)
        {
            count -= amount;
        }
    }
}

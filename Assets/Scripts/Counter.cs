using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WorldsApart
{
    public class Counter : MonoBehaviour
    {
        public int count;

        public void Increment(int amount = 1)
        {
            count += amount;
        }

        public void Decrement(int amount = 1)
        {
            count -= amount;
        }
    }
}

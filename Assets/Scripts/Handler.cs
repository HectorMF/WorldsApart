using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WorldsApart
{
    public abstract class Handler : MonoBehaviour
    {
        public abstract void invoke();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using WorldsApart.Utility;

namespace WorldsApart.Scripting
{
    public class WanderScript : Script
    {
        public GameObject gameObject;
        public bool value;

        public override void Start(Action OnFinish)
        {
            var wander = gameObject.GetComponent<Wander>();
            wander.waiting = true;
            if (wander != null && wander.enabled != value) wander.enabled = value;
            OnFinish();
        }
    }
}

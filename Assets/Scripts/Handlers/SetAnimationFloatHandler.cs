using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WorldsApart.Handlers
{
    [Serializable]
    public class SetAnimationFloatHandler : Handler
    {
        public string floatName;
        public float value;

        public override void innerDelegate()
        {
            var animator = gameObject.GetComponent<Animator>();
            if (animator != null) animator.SetFloat(floatName, value);
        }
    }
}

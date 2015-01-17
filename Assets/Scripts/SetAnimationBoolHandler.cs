using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WorldsApart
{
    [Serializable]
    public class SetAnimationBoolHandler : Handler
    {
        public string booleanName;
        public bool value;

        public override void invoke()
        {
            var animator = gameObject.GetComponent<Animator>();
            if (animator != null) animator.SetBool(booleanName, value);
        }
    }
}

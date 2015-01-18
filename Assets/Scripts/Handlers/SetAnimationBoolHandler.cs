using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WorldsApart.Handlers
{
    [Serializable]
    public class SetAnimationBoolHandler : Handler
    {
        public string booleanName;
        public bool value;

        public override void innerDelegate()
        {
            try
            {
                var animator = gameObject.GetComponent<Animator>();
                if (animator != null) animator.SetBool(booleanName, value);
            }
            catch(Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}

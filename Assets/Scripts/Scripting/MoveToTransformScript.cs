using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WorldsApart.Scripting
{
    [Serializable]
    public class MoveToTransformScript : Script
    {
        public Transform target;
        public GameObject gameObject;

        public override void Start(Action OnFinish)
        {
			gameObject.GetComponent<Movement>().Move(target, OnFinish);
        }
    }
}

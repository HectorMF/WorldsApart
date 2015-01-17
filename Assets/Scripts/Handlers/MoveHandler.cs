using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WorldsApart.Handlers
{
    public class MoveHandler : Handler
	{
		public Vector3 target;
        public GameObject targetObject;

        public override void innerDelegate()
        {
			targetObject.GetComponent<Movement>().Move(target);
        }
    }
}

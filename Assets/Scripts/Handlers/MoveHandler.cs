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

        private Movement movement;

        public override void innerDelegate()
        {
            if(movement == null && targetObject != null) movement = targetObject.GetComponent<Movement>();
            if(movement != null) movement.Move(target);
        }
    }
}

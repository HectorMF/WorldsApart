using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WorldsApart.Handlers
{
    public class MoveHandler : Handler
	{
		Vector3 target;

        public override void innerDelegate()
        {
			gameObject.GetComponent<Movement>().Move(target);
        }
    }
}

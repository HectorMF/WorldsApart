using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WorldsApart.Handlers
{
    public class MoveHandler : Handler
    {
        public override void invoke()
        {
            gameObject.transform.position += new UnityEngine.Vector3(1f, 0f, 0f);
        }
    }
}

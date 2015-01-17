using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WorldsApart.Handlers
{
    public class DeactivateHandler : Handler
    {
        public override void invoke()
        {
            gameObject.SetActive(false);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Vexe.Runtime.Types;
using WorldsApart.Clickables;
using WorldsApart.Handlers;
using WorldsApart.Utility;

namespace WorldsApart
{
    public class MiniGameObjectGenerator : ObjectGenerator
    {
        public Counter counter;

        public override void Generate(GameObject origin)
        {
            var obj = Instantiate(origin, bounds.getRandomPosition(), Quaternion.identity) as GameObject;
            var mClickable = obj.GetComponent<MinigameClickable>();
            if (mClickable != null) mClickable.counter = counter;
        }
    }
}

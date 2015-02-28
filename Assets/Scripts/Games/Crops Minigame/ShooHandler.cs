using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using WorldsApart.Handlers;
using WorldsApart.Utility;

namespace WorldsApart.Games.CropsMinigame
{
    public class ShooHandler : Handler
    {
        public int clickRequirement;
        public BoundingBox bounds;
        public Vector3 center;
        private int clicks = 0;

        public override void innerDelegate()
        {
            clicks++;
            if (clicks >= clickRequirement)
            {
                var handler = new MoveHandler();
                handler.gameObject = gameObject;
                handler.target = bounds.getClosestOutOfBounds(center, gameObject.transform.position, WorldsApart.Utility.BoundingBox.Axis.Horizontal);
                handler.onFinished = () =>
                {
                    var innerHandler = new DeactivateHandler();
                    innerHandler.gameObject = gameObject;
                    innerHandler.Invoke();
                };
                handler.Invoke();
            }
        }
    }
}

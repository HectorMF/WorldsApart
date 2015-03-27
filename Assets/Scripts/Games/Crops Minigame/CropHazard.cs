using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using WorldsApart.Handlers;
using WorldsApart.Utility;

namespace WorldsApart.Games.CropsMinigame
{
    public class CropHazard : MonoBehaviour
    {
        public BoundingBox bounds;
        public Vector3 center;
        private bool moving = false;

        void Update()
        {
            if (!moving)
            {
                var handler = new MoveHandler();
                handler.gameObject = gameObject;
                handler.target = bounds.getRandomPosition(center) + new Vector3(0, 0, -1);
                handler.onFinished = () => moving = false;
                moving = true;
                handler.Invoke();
            }
        }

        void OnTriggerEnter(Collider col)
        {
            if (col.tag == "Crop")
            {
                var handler = new DeactivateHandler();
                handler.gameObject = col.gameObject;
                handler.Invoke();
            }
        }
    }
}

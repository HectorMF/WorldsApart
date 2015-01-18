using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WorldsApart.Utility
{
    [Serializable]
    public class BoundingBox
    {
        public float height;
        public float width;

        public Vector3 getRandomPosition(Vector3 position)
        {
            var x = UnityEngine.Random.Range(-width/2, width/2) + position.x;
            var y = UnityEngine.Random.Range(-height/2, height/2) + position.y;

            return new Vector3(x, y, position.z);
        }
    }
}

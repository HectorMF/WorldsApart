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
        public float top;
        public float bottom;
        public float left;
        public float right;
        public float z;

        public Vector3 getRandomPosition()
        {
            var x = UnityEngine.Random.Range(left, right);
            var y = UnityEngine.Random.Range(top, bottom);

            return new Vector3(x, y, z);
        }
    }
}

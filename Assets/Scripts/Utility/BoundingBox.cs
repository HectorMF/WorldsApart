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

        public Vector3 getClosestOutOfBounds(Vector3 center, Vector3 position)
        {
            if (position.x < center.x)
            {
                if (position.y < center.y)
                {
                    if (position.x - (center.x - width / 2) < (center.y - height / 2) - position.y)
                    {
                        return new Vector3(center.x - width / 2 - 2f, position.y, position.z);
                    }
                    else
                    {
                        return new Vector3(position.x, center.y - height / 2 - 2f, position.z);
                    }
                }
                else
                {
                    if (position.x - (center.x - width / 2) < position.y - (center.y + height / 2))
                    {
                        return new Vector3(center.x - width / 2 - 2f, position.y, position.z);
                    }
                    else
                    {
                        return new Vector3(position.x, center.y + height / 2 + 2f, position.z);
                    }
                }
            }
            else
            {
                if (position.y < center.y)
                {
                    if ((center.x + width / 2) - position.x < (center.y - height / 2) - position.y)
                    {
                        return new Vector3(center.x + width / 2 + 2f, position.y, position.z);
                    }
                    else
                    {
                        return new Vector3(position.x, center.y - height / 2 - 2f, position.z);
                    }
                }
                else
                {
                    if ((center.x - width / 2) - position.x < position.y - (center.y + height / 2))
                    {
                        return new Vector3(center.x + width / 2 + 2f, position.y, position.z);
                    }
                    else
                    {
                        return new Vector3(position.x, center.y + height / 2 + 2f, position.z);
                    }
                }
            }
        }

        public Vector3 getRandomOutOfBounds(Vector3 center)
        {
            float random = UnityEngine.Random.Range(-1, 1);
            if(random < 0)
            {
                float displacement = UnityEngine.Random.Range(-height / 2, height / 2);

                if (random < -.5f)
                    return new Vector3(center.x - width / 2 - 2f, displacement + center.y, center.z);
                else
                    return new Vector3(center.x + width / 2 + 2f, displacement + center.y, center.z);
            }
            else
            {
                float displacement = UnityEngine.Random.Range(-width / 2, width / 2);

                if (random < -.5f)
                    return new Vector3(displacement + center.x, center.y - height/2 - 2f, center.z);
                else
                    return new Vector3(displacement + center.x, center.y + height / 2 + 2f, center.z);
            }
        }
    }
}

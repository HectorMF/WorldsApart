using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Vexe.Runtime.Types;
using WorldsApart.Clickables;
using WorldsApart.Handlers;

namespace WorldsApart.Utility
{
    public class ObjectGenerator : BetterBehaviour
    {
        public BoundingBox bounds;
        public float waitTime;
        public Transform parentCollection;

        public List<Tuple<GameObject, float>> objects;

        private float timeLeft;
        private float total;

        void Start()
        {
            total = objects.Sum(t => t.Item2);
            timeLeft = waitTime;
        }

        void Update()
        {
            timeLeft -= UnityEngine.Time.deltaTime;
            if (timeLeft <= 0)
            {
                GameObject origin;

                timeLeft = waitTime;
                if (objects.Count > 1)
                {
                    var random = UnityEngine.Random.Range(0f, total);
                    var floatList = objects.Select(t => t.Item2).ToList();
                    int i = 0;

                    random -= floatList[i];
                    while (random > 0)
                    {
                        i++;
                        random -= floatList[i];
                    }

                    origin = objects.Select(t => t.Item1).ToList()[i];
                }
                else
                    origin = objects.Select(t => t.Item1).First();
                if (origin != null)
                {
                    Generate(origin);
                }
            }
        }

        public virtual void Generate(GameObject origin)
        {
            var obj = Instantiate(origin, bounds.getRandomPosition(transform.position), Quaternion.identity) as GameObject;
            obj.transform.parent = parentCollection;
        }

        public void reset()
        {
            timeLeft = waitTime;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position + new Vector3(bounds.width / 2, bounds.height/2, 0f), transform.position + new Vector3(bounds.width / 2, -bounds.height/2, 0f));
            Gizmos.DrawLine(transform.position + new Vector3(bounds.width / 2, bounds.height / 2, 0f), transform.position + new Vector3(-bounds.width / 2, bounds.height / 2, 0f));
            Gizmos.DrawLine(transform.position - new Vector3(bounds.width / 2, bounds.height / 2, 0f), transform.position - new Vector3(bounds.width / 2, -bounds.height / 2, 0f));
            Gizmos.DrawLine(transform.position - new Vector3(bounds.width / 2, bounds.height / 2, 0f), transform.position - new Vector3(-bounds.width / 2, bounds.height / 2, 0f));
        }
    }
}

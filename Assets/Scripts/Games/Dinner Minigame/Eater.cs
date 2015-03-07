using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WorldsApart.Games.DinnerMinigame
{
    public class Eater : MonoBehaviour
    {
        public Plate plate;
        public float time;
        private float timePassed;

        private const int maxMood = 5;
        private int mood;
        private MeshRenderer renderer;

        void Start()
        {
            mood = maxMood;
            renderer = GetComponent<MeshRenderer>();
            timePassed = time;
        }

        void Update()
        {
            timePassed -= Time.deltaTime;

            if (timePassed <= 0)
            {
                if (plate.Eat())
                {
                    if (mood < maxMood) mood++;
                }
                else if (mood > 0) mood--;
                timePassed = time;
            }

            renderer.material.color = (Color.blue / maxMood) * mood;
        }
    }
}

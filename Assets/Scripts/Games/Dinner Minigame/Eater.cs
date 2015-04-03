using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using WorldsApart.GUI;

namespace WorldsApart.Games.DinnerMinigame
{
    public class Eater : MonoBehaviour
    {
        public Plate plate;
        public float time;
        public DinnerGameManager manager;
        [HideInInspector]
        public int mood;
        internal bool started;

        private float timePassed;

        private const int maxMood = 4;
        private SpriteRenderer renderer;
        private Wobble wobble;

        void Start()
        {
            mood = maxMood;
            renderer = GetComponentsInChildren<SpriteRenderer>()[2];
            wobble = GetComponentInChildren<Wobble>();
            timePassed = time;
        }

        void Update()
        {
            if (!started)
            {
                if (manager.started) started = true;
                else return;
            }

            timePassed -= Time.deltaTime;

            if (timePassed <= 0)
            {
                if (plate.Eat())
                {
                    if (mood < maxMood)
                    {
                        mood++;
                        renderer.sprite = manager.getMoodSprite(mood);
                        wobble.TriggerWobble();
                    }
                }
                else if (mood > 0)
                { 
                    mood--;
                    renderer.sprite = manager.getMoodSprite(mood);
                    wobble.TriggerWobble();
                }
                timePassed = time;
            }
        }
    }
}

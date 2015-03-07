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

        private float timePassed;

        private const int maxMood = 4;
        private int mood;
        private SpriteRenderer renderer;
        private Wobble wobble;

        void Start()
        {
            mood = maxMood;
            renderer = GetComponentsInChildren<SpriteRenderer>()[1];
            wobble = GetComponentInChildren<Wobble>();
            timePassed = time;
        }

        void Update()
        {
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

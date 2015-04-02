using UnityEngine;
using System.Collections;
using System.Linq;
using WorldsApart;
using WorldsApart.Utility;
using WorldsApart.Cameras;
using System.Collections.Generic;
using Vexe.Runtime.Types;
using System;
using UnityEngine.UI;
using DG.Tweening;

namespace WorldsApart.Games.CropsMinigame
{
    public class CropsGameController : BetterBehaviour
    {
        public List<Hazard> Hazards;
        public float spawnFrequency;
        public BoundingBox bounds;

        public Text timer;
        public float time = 60f;
        private int seconds;
        private int minutes;
        private int oldSeconds;
        private float timePassed = 0f;

        ScoreController scoreController;
        private bool started;

        void Start()
        {
            Fader.FadeToClear(Fader.Gesture.Tap, 2, 2, "Pick the Crops", "Shoo away the animals", () => started = true);

            GameObject scoreObject = GameObject.Find("ScoreController");
            if (scoreObject != null)
                scoreController = scoreObject.GetComponent<ScoreController>();

            minutes = (int)(time / 60);
            seconds = (int)(time % 60);
            if (timer != null) timer.text = minutes + ":" + seconds.ToString("00");
        }

        void Update()
        {
            if (!started) return;
            if (time <= 0) return;
            
            oldSeconds = seconds;

            //decrement the timer, and calculate minutes and seconds as integers
            time -= Time.deltaTime;
            minutes = (int)(time / 60);
            seconds = (int)(time % 60);

            //instead of updating every frame, update every second change
            if (seconds != oldSeconds)
            {
                if(timer != null) timer.text = minutes + ":" + seconds.ToString("00");

                if (minutes == 0 && seconds <= 10)
                {
                    if (timer != null)
                    {
                        timer.DOColor(Color.red, .5f).SetLoops(2, LoopType.Yoyo);
                        timer.gameObject.transform.DOScale(new Vector3(1.5f, 1.5f, 1), .5f).SetLoops(2, LoopType.Yoyo);
                    }
                }
            }

            if (time <= 0)
                Fader.FadeToBlack(0, 2, "", "", EndGame);


            timePassed += Time.deltaTime;

            if (timePassed > spawnFrequency)
            {
                float sum = Hazards.Sum(x => x.probability);
                float selection = UnityEngine.Random.Range(0f, sum);
                for (int i = 0; i < Hazards.Count; i++)
                {
                    selection -= Hazards[i].probability;
                    if (selection <= 0)
                    {
                        Vector3 spawnLocation = bounds.getRandomOutOfBounds(transform.position, BoundingBox.Axis.Horizontal) + new Vector3(0, 0, -1);
                        Instantiate(Hazards[i].prefab, spawnLocation, Hazards[i].prefab.transform.rotation);
                        break;
                    }
                }
                timePassed = 0f;
            }
        }

        [Serializable]
        public class Hazard
        {
            public GameObject prefab;
            public float probability;
        }

        public void EndGame()
        {
            if (scoreController != null) scoreController.Mood = CounterManager.Instance.GetCounter("HarvestCount").count;

            Application.LoadLevel("WorldsApart");
        }
    }
}
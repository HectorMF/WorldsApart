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
        public GameObject miniGame;
        public float speed = 20.0f;
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

        void Start()
        {
            Fader.FadeToClear(2, 2, "Pick the Crops");

            GameObject scoreObject = GameObject.Find("ScoreController");
            if (scoreObject != null)
                scoreController = scoreObject.GetComponent<ScoreController>();

            StartGame();
        }

        void Update()
        {
            if (time <= 0)
            {
                this.enabled = false;
                return;
            }

            oldSeconds = seconds;

            //decrement the timer, and calculate minutes and seconds as integers
            time -= Time.deltaTime;
            minutes = (int)(time / 60);
            seconds = (int)(time % 60);

            //instead of updating every frame, update every second change
            if (seconds != oldSeconds)
            {
                timer.text = minutes + ":" + seconds.ToString("00");

                if (minutes == 0 && seconds <= 10)
                {
                    timer.DOColor(Color.red, .5f).SetLoops(2, LoopType.Yoyo);
                    timer.gameObject.transform.DOScale(new Vector3(1.5f, 1.5f, 1), .5f).SetLoops(2, LoopType.Yoyo);
                }
            }

            if (seconds <= 0)
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
                        Vector3 spawnLocation = bounds.getRandomOutOfBounds(transform.position, BoundingBox.Axis.Horizontal);
                        Instantiate(Hazards[i].prefab, spawnLocation, Quaternion.identity);
                    }
                }
                timePassed = 0f;
            }
        }

        public void StartGame()
        {
            miniGame.SetActive(true);
            miniGame.GetComponent<ObjectGenerator>().enabled = true;
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
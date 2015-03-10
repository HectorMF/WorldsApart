using UnityEngine;
using System.Collections;
using System.Linq;
using WorldsApart;
using WorldsApart.Utility;
using WorldsApart.Cameras;
using System.Collections.Generic;
using Vexe.Runtime.Types;
using System;

namespace WorldsApart.Games.CropsMinigame
{
    public class CropsGameController : BetterBehaviour
    {
        public GameObject miniGame;
        public float speed = 20.0f;

        public List<Hazard> Hazards;
        public float spawnFrequency;
        public BoundingBox bounds;

        private float timePassed = 0f;

        ScoreController scoreController;

        void Start()
        {
            GameObject scoreObject = GameObject.Find("ScoreController");
            if (scoreObject != null)
                scoreController = scoreObject.GetComponent<ScoreController>();

            StartGame();
        }

        void Update()
        {
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

            Application.LoadLevel("WorldsApartAgain");
        }
    }
}
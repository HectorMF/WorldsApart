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
        Vector3 previousPosition;
        public GameObject miniGame;
        public float speed = 20.0f;
        float lerpTime;

        public List<Hazard> Hazards;
        public float spawnFrequency;
        public BoundingBox bounds;

        private float timePassed = 0f;

        int scoreGoal = 5;

        public enum MiniGameState
        {
            Suspended,
            Starting,
            Playing,
            Ending
        }

        void Start()
        {
            StartGame();
        }

        public MiniGameState CurrentState;

        void Update()
        {
            switch (CurrentState)
            {
                case MiniGameState.Starting:
                    miniGame.GetComponent<ObjectGenerator>().enabled = true;
                    CurrentState = MiniGameState.Playing;
                    break;
                case MiniGameState.Playing:
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
                    if (CounterManager.Instance.GetCounter("GardenMiniGame").count >= scoreGoal)
                    {
                        CurrentState = MiniGameState.Ending;
                        lerpTime = 0;
                    }
                    break;
                case MiniGameState.Ending:
                    ThirdWorldManager.Instance.Report();
                    CurrentState = MiniGameState.Suspended;
                    break;
            }
        }

        public void StartGame()
        {
            //if (CanAndShouldWater())
            //{
            CurrentState = MiniGameState.Starting;
            lerpTime = 0;
            miniGame.SetActive(true);
            //scoreGoal =  gameObject.GetComponent<Thirst>().Drink();
            //}
        }

        bool CanAndShouldWater()
        {
            return ThirdWorldManager.Instance.AnyWater
                && gameObject.GetComponent<Thirst>().AmountDrank < gameObject.GetComponent<Thirst>().TotalRequiredWater
                    && ThirdWorldManager.Instance.TryAction();
        }

        [Serializable]
        public class Hazard
        {
            public GameObject prefab;
            public float probability;
        }
    }
}
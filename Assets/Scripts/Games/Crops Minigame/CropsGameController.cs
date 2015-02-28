using UnityEngine;
using System.Collections;
using WorldsApart;
using WorldsApart.Utility;
using WorldsApart.Cameras;

namespace WorldsApart.Games.CropsMinigame
{
    public class CropsGameController : MonoBehaviour
    {
        Vector3 previousPosition;
        public GameObject miniGame;
        public float speed = 20.0f;
        float lerpTime;

        int scoreGoal;

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
                    break;
                case MiniGameState.Playing:
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
    }
}
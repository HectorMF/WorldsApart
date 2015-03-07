using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace WorldsApart.GUI
{
    public class GUIWater : MonoBehaviour
    {
        public float easeTime = 1;

        private float oldValue;
        private float currentValue;
        private Slider slider;
        private float easeTimer;
        private bool easing;
        public enum GameMode
        {
            ThirdWorld,
            WaterMiniGame,
            FarmingMiniGame,
            MilkingMiniGame
        }
        public GameMode CurrentGameMode;
        // Use this for initialization
        void Start()
        {
            slider = this.GetComponent<Slider>();
            if (slider != null)
                slider.maxValue = 20;
            currentValue = slider.value;
            oldValue = slider.value;
            easing = false;
            if (CurrentGameMode == GameMode.ThirdWorld)
            {
                ThirdWorldManager.OnHasPackChanged += (value) => slider.maxValue = value;
            }
            else
            {
                slider.maxValue = WaterGameLogic.Instance.maxWater;
            }
            
        }

        // Update is called once per frame
        void Update()
        {
            bool check = false;
            float currentWater = 0f;
            //HACKISH INTERFACE THE MANAGERS OUT! (TO MY SELF!)
            switch(CurrentGameMode)
            {
                case GameMode.ThirdWorld:
                    check = easing && slider.value != ThirdWorldManager.Instance.CurrentWater;
                    currentWater = ThirdWorldManager.Instance.CurrentWater;
                    break;
                case GameMode.WaterMiniGame: 
                    check = easing && slider.value != WaterGameLogic.Instance.water;
                    currentWater = WaterGameLogic.Instance.water;
                    break;
            }
            if (!check)
            {
                oldValue = slider.value;
                currentValue = currentWater;
                easeTimer = 0;
                easing = true;
            }

            if (easing)
            {
                easeTimer += Time.deltaTime / easeTime;
                if (easeTimer > 1)
                {
                    easing = false;
                    slider.value = currentWater;
                }
                else
                    slider.value = currentValue;//Mathf.Lerp(oldValue, currentValue, easeTimer);
            }
        }
    }
}
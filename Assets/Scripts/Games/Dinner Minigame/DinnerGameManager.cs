using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Vexe.Runtime.Types;

namespace WorldsApart.Games.DinnerMinigame
{
    public class DinnerGameManager : BetterBehaviour
    {
        public Text timer;
        private float time;
        private int seconds;
        private int minutes;
        private int oldSeconds;
        internal bool started;
		public float playTime = 60f;
		private float countDownTime = 4f;

        public List<Sprite> moods;
        public List<List<Sprite>> plates;

        private List<Eater> eaters;
        ScoreController scoreController;

        void Start()
        {
            GameObject scoreObject = GameObject.Find("ScoreController");
            if (scoreObject != null)
                scoreController = scoreObject.GetComponent<ScoreController>();
			started = false;
			time = countDownTime;
           // Fader.FadeToClear(Fader.Gesture.Tap, 2, 2, "Feed Your Family", "Click the Plates", () => started = true);

            minutes = (int)(time / 60);
            seconds = (int)(time % 60);
			//minutes + ":" + seconds.ToString("00");
			//timer.DOColor(Color.red, .5f).SetLoops(2, LoopType.Yoyo);

            eaters = new List<Eater>();
        }

        void Update()
        {
            //if (!started) return;
            oldSeconds = seconds;

            //decrement the timer, and calculate minutes and seconds as integers
            time -= Time.deltaTime;
            minutes = (int)(time / 60);
            seconds = (int)(time % 60);

            //instead of updating every frame, update every second change
            if (seconds != oldSeconds)
            {
				if (!started){
					CountDown();
				}
				else{ 
					timer.text = minutes + ":" + seconds.ToString("00");
				
					if (minutes == 0 && seconds <= 10 && started)
					{
						timer.DOColor(Color.red, .5f).SetLoops(2, LoopType.Yoyo);
						timer.gameObject.transform.DOScale(new Vector3(1.5f, 1.5f, 1), .5f).SetLoops(2, LoopType.Yoyo);
					} 
					if (seconds <= 0){
						EndGame();
						started = false;
					}
				}
            }

//            if (seconds <= 0 && started){
//				EndGame();
//				started = false;
//			}
        }

        public Sprite getMoodSprite(int mood)
        {
            return moods[mood];
        }

        public void RegisterEater(Eater eater)
        {
            eaters.Add(eater);
        }

        public void EndGame()
        {
            if(scoreController != null) scoreController.Mood = eaters.Sum(e => e.mood);

			Fader.Instance.SetTitle("You have gained +" + "10" + " Mood.")
				.FadeOutOnComplete(()=>
					{
						Application.LoadLevel("WorldsApart");
					})
				.FadeInOnComplete(()=>ThirdWorldManager.Instance.UsedAction())
				.FadeOutIn();
        }

        internal List<Sprite> GetPlateSprites()
        {
            return plates[UnityEngine.Random.Range(0, plates.Count)];
        }

		void CountDown(){
			if (seconds == 3){
				timer.text = "Ready";
				timer.DOColor(Color.red, .5f).SetLoops(2, LoopType.Yoyo);
			}
			else if (seconds == 2) {
				timer.text = "Set";
				timer.DOColor(Color.yellow, .5f).SetLoops(2, LoopType.Yoyo);
			}
			else if (seconds == 1){
				timer.text = "Go!";
				timer.DOColor(Color.green, .5f).SetLoops(2, LoopType.Yoyo);
			}
			else if (seconds == 0){
				started = true;
				time = playTime;
			}
		}
	}
}

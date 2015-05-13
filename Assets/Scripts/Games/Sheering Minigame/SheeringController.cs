﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace WorldsApart.Games.SheeringMinigame
{
    public class SheeringController : MonoBehaviour
    {
        public int rows;
        public int columns;
        public float spriteHeight;
        public float spriteWidth;
        private static List<string> puzzles = new List<string> 
        {
            "000000000001111100001000001000000000010000010000001110011000000000",
            "000001000000111110001000001001100000010001010000111110011100000000",
            "000000000000001100000010000000100001110011000001100110000011001111",
            "0011000000000000000000000000100000000000011000000000000111000000000",
            "000000000001100000000000110000000000000001100000000011010000000000"
        };
        private string puzzle;

        public List<Sheerable> sheerablePrefabs;

        private int prevIndex;
        private List<int> path;
        private bool mouseIsActive;

        ScoreController scoreController;

		public Text timer;
		public float playTime = 30f;
		private float countDownTime = 4f;
		private float time;
		private int seconds;
		private int minutes;
		private int oldSeconds;
		private bool started;

		private RectTransform canvas;
		private Vector3 originalPos;
		private Vector3 centerPos;


        void Start()
        {
            puzzle = puzzles[UnityEngine.Random.Range(0, puzzles.Count)];

            GameObject scoreObject = GameObject.Find("ScoreController");
            if (scoreObject != null)
                scoreController = scoreObject.GetComponent<ScoreController>();

            prevIndex = -1;
            path = new List<int>();

            for (int x = 0; x < columns; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    if (puzzle[y * columns + x] == '1') continue;
                    var suitable = sheerablePrefabs;
                    if (x == 0 && y == 0) suitable = sheerablePrefabs.Where(p => p.lowerLeft).ToList();
                    if (x == 0 && y == rows - 1) suitable = sheerablePrefabs.Where(p => p.upperLeft).ToList();
                    if (x == columns - 1 && y == rows - 1) suitable = sheerablePrefabs.Where(p => p.upperRight).ToList();
                    if (x == columns - 1 && y == 0) suitable = sheerablePrefabs.Where(p => p.lowerRight).ToList();

                    var prefab = suitable[UnityEngine.Random.Range(0, suitable.Count)];

                    var block = Instantiate(prefab, new Vector3(((float)x + .5f - ((float)columns / 2f)) * spriteWidth + transform.position.x, ((float)y + .5f - ((float)rows / 2f)) * spriteHeight + transform.position.y, transform.position.z), Quaternion.identity) as Sheerable;
                    block.transform.parent = transform;
                    block.setController(this);
                    block.index = y * columns + x;
                }
            }
			// Added for count down
			started = false;
			time = countDownTime;
			canvas = (RectTransform)timer.rectTransform.parent;
			originalPos = timer.rectTransform.position;
			centerPos = new Vector3 ((float) -canvas.rect.width * canvas.lossyScale.x, (float)canvas.rect.height * canvas.lossyScale.y, 0f);
			timer.rectTransform.localPosition = centerPos;
        }

		void Update () {
			if (time <= 0 && started) {
				this.enabled = false;
				EndGame("Out of Time", false);
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
				if(!started)
					CountDown();
				else
				{
					timer.text = minutes + ":" + seconds.ToString("00");
					
					if (minutes == 0 && seconds <= 10)
					{
						timer.DOColor(Color.red, .5f).SetLoops(2, LoopType.Yoyo);
						timer.gameObject.transform.DOScale(new Vector3(1.5f, 1.5f, 1), .5f).SetLoops(2, LoopType.Yoyo);
					}
				}
			}
		}

		public bool Started
		{
			get
			{
				return started;
			}
		}

        public void setMouseActive(bool value)
        {
            mouseIsActive = value;
        }

        public bool getMouseActive()
        {
            return mouseIsActive;
        }

        public void registerSelected(int index)
        {
            path.Add(index);
            prevIndex = index;
            if (path.Count == rows * columns - puzzle.Count(c => c=='1'))
            {
                if (scoreController != null) scoreController.Mood = 2;
                EndGame("You Win!!!", true);
            }
            else if (!NodesAvailable(index))
            {
                if (scoreController != null) scoreController.Mood = 0;
                EndGame("You Lost.", false);
            }
        }

        internal bool ValidIndex(int index)
        {
            if (prevIndex == -1) return true;
            if(path.Contains(index)) return false;
            //Look left
            if (prevIndex % columns != columns - 1 && prevIndex == index - 1) return true;
            //Look right
            if (prevIndex % columns != 0 && prevIndex == index + 1) return true;
            //Look up
            if (prevIndex < columns * (rows - 1) && prevIndex == index - columns) return true;
            //Look down
            if (prevIndex > columns - 1 && prevIndex == index + columns) return true;
            return false;
        }

        private bool NodesAvailable(int index)
        {
            if (index % columns != columns - 1 && !path.Contains(index + 1) && puzzle[index+1] != '1') return true;
            if (index % columns != 0 && !path.Contains(index - 1) && puzzle[index - 1] != '1') return true;
            if (index < columns * (rows - 1) && !path.Contains(index + columns) && puzzle[index + columns] != '1') return true;
            if (index > columns - 1 && !path.Contains(index - columns) && puzzle[index - columns] != '1') return true;
            return false;
        }

        private void EndGame(string text, bool win)
        {
            if (win)
            {
                ThirdWorldManager.Instance.IncrementMood();
            }
            else
                ThirdWorldManager.Instance.DecrementMood();

			Fader.Instance
				.SetTitle(text)
				.FadeOutOnComplete(()=>
				{
					Application.LoadLevel("WorldsApart");
				})
				.FadeInOnComplete(()=>ThirdWorldManager.Instance.UsedAction())
				.FadeOutIn();
        }
		private void CountDown()
		{
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
				minutes = (int)(time / 60);
				seconds = (int)(time % 60);
				timer.rectTransform.position = originalPos;
			}
		}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WorldsApart.Games.DinnerMinigame
{
    public class DinnerGameManager : MonoBehaviour
    {
        public List<Sprite> moods;
        private List<Eater> eaters;
        ScoreController scoreController;

        void Start()
        {
            GameObject scoreObject = GameObject.Find("ScoreController");
            if (scoreObject != null)
                scoreController = scoreObject.GetComponent<ScoreController>();
            
            eaters = new List<Eater>();
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
                
            Application.LoadLevel("WorldsApartAgain");
        }
    }
}

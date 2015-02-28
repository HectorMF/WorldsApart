using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace WorldsApart.GUI
{
    public class Happiness : MonoBehaviour
    {

        public Sprite Ecstatic;
        public Sprite Happy;
        public Sprite Neutral;
        public Sprite Sad;
        public Sprite Depressed;
        public Image currentImage;

        void Start()
        {
            currentImage = this.GetComponent<Image>();
        }

        void Update()
        {
            var mood = ThirdWorldManager.Instance.CurrentMood;
            if (mood == null)
            {
                //TODO: First World Manager
            }

            switch (mood)
            {
                case (Mood.MoodNames.Ecstatic):
                    currentImage.sprite = Ecstatic;
                    break;
				case (Mood.MoodNames.Happy):
                    currentImage.sprite = Happy;
                    break;
				case (Mood.MoodNames.Neutral):
                    currentImage.sprite = Neutral;
                    break;
				case (Mood.MoodNames.Sad):
                    currentImage.sprite = Sad;
                    break;
				case (Mood.MoodNames.Depressed):
                    currentImage.sprite = Depressed;
                    break;
            }
        }
    }
}
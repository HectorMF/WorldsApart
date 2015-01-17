using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Happiness : MonoBehaviour {

    public Sprite Ecstatic;
    public Sprite Happy;
    public Sprite Neutral;
    public Sprite Sad;
    public Sprite Depressed;
    public Image currentImage;
	// Use this for initialization
	void Start () {
        currentImage = this.GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        var mood = ThirdWorldManager.Instance.CurrentMood;


        switch(mood)
        {
            case(ThirdWorldManager.Mood.Ecstatic):
                currentImage.sprite = Ecstatic;
                break;
            case(ThirdWorldManager.Mood.Happy):
                currentImage.sprite = Happy;
                break;
            case (ThirdWorldManager.Mood.Neutral):
                currentImage.sprite = Neutral;
                break;
            case (ThirdWorldManager.Mood.Sad):
                currentImage.sprite = Sad;
                break;
            case (ThirdWorldManager.Mood.Depressed):
                currentImage.sprite = Depressed;
                break;           
        }

	}
}

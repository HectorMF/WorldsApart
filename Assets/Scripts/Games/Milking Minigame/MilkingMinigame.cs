using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class MilkingMinigame : MonoBehaviour {
    public Text timer;
    public float time = 60f;
    private int seconds;
    private int minutes;
    private int oldSeconds;

	void Update () {
        if (time <= 0) {
            this.enabled = false;
			Fader.FadeOutIn(Fader.Gesture.None, "You have gained +" + "10" + " Food.", "",()=>Application.LoadLevel("WorldsApart"));
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
	}
}

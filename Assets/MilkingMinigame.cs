using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class MilkingMinigame : MonoBehaviour {
    public Text timer;
    public float time = 60f;
    private int seconds;
    private int minutes;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (time <= 0) return;
        time -= Time.deltaTime;
        int oldSeconds = seconds;
        minutes = (int)(time / 60.0);
        seconds = (int)(time % 60.0);
        if (seconds != oldSeconds && seconds <= 10)
        {
            timer.DOColor(Color.red, .5f).SetLoops(2, LoopType.Yoyo);
            timer.gameObject.transform.DOScale(new Vector3(1.5f, 1.5f, 1), .5f).SetLoops(2, LoopType.Yoyo);
        }
        timer.text = minutes + ":" + seconds.ToString("00");
        
        
        
	}
}

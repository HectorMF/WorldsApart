using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class PumpingMinigame : MonoBehaviour {
	public Text timer;

	private enum State {Starting, Started, Finishing, Finished}
	private State currentState;

	private float countDownTime = 6f;
	private float playTime = 20f;

	private int minutes;
	private int seconds;
	private int oldSeconds;

	private GUIMeter meterScript;

	void Start ()
	{
		//Fader.FadeToClear(2,2,"Pump Water", "Time pumps for max water");
		currentState = State.Starting;
		meterScript = GetComponentInChildren<GUIMeter>();
		meterScript.enabled = false;
	}

	void Update ()
	{
		switch (currentState)
		{
		case State.Starting:
			if (countDownTime <= 0) {
				currentState = State.Started;
				meterScript.enabled = true;
			}
			else 
				countDownTime = UpdateTimer(countDownTime);
			break;
		case State.Started:
			if (playTime <= 0){
				currentState = State.Finishing;
			}
			else 
				playTime = UpdateTimer(playTime);
			break;
		case State.Finishing:
			Fader.FadeOutIn(Fader.Gesture.None,0,2,"Game Over","",()=>Application.LoadLevel("WaterGame"));
			meterScript.enabled = false;
			currentState = State.Finished;
			break;
		case State.Finished:
			this.enabled = false;
			Invoke("EndGame", 3);
			break;
		}
	}

	private float UpdateTimer (float time) 
	{
		oldSeconds = seconds;

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
		return time;
	}
}












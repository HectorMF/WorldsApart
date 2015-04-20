using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class PumpingMinigame : MonoBehaviour {
	public Text timer;

	enum State {Starting, Started, Finishing, Finished}
	State currentState;

	float countDownTime = 3f;
	float playTime = 20f;

	float time;
	int minutes;
	int seconds;
	int oldSeconds;

	GUIMeter meterScript;

	void Start ()
	{
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
				UpdateTimer();
			break;
		case State.Started:
			if (playTime <= 0){
				currentState = State.Finishing;
			}
			else 
				UpdateTimer();
			break;
		case State.Finishing:
			Fader.Instance
				.SetTitle(string.Format("You gained {0} water!", meterScript.GetWaterPumped()))
				.FadeOutOnComplete(()=>
					{
						Application.LoadLevel("Watergame");
					})
				.FadeOutIn();
			meterScript.enabled = false;
			currentState = State.Finished;
			break;
		case State.Finished:
			this.enabled = false;
			break;
		}
	}

	void UpdateTimer () 
	{
		oldSeconds = seconds;
		if (currentState == State.Starting){
			countDownTime -= Time.deltaTime;
			time = countDownTime;
		} 
		else {
			playTime -= Time.deltaTime;
			time = playTime;
		}
		minutes = (int)(time / 60);
		seconds = (int)(time % 60);
		
		//instead of updating every frame, update every second change
		if (seconds != oldSeconds)
		{
			if (currentState == State.Starting && seconds == 2){
				timer.text = "Ready";
				timer.DOColor(Color.red, .5f).SetLoops(2, LoopType.Yoyo);
			}
			else if (currentState == State.Starting && seconds == 1) {
				timer.text = "Set";
				timer.DOColor(Color.yellow, .5f).SetLoops(2, LoopType.Yoyo);
			}
			else if (currentState == State.Starting && seconds == 0){
				timer.text = "Go!";
				timer.DOColor(Color.green, .5f).SetLoops(2, LoopType.Yoyo);
			}
			else 
				timer.text = minutes + ":" + seconds.ToString("00");
			
			if (minutes == 0 && seconds <= 10 && currentState != State.Starting)
			{
				timer.DOColor(Color.red, .5f).SetLoops(2, LoopType.Yoyo);
				timer.gameObject.transform.DOScale(new Vector3(1.5f, 1.5f, 1), .5f).SetLoops(2, LoopType.Yoyo);
			}
		}
	}
}












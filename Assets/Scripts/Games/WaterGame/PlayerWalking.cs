using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerWalking : MonoBehaviour {
    public float StepSizeAKASpeed = 1f;
    public float totalDistance = 100f;
	public Text timer;
	public float time = 3f;
	int minutes;
	int seconds;
	int oldSeconds;
	Animator anim;
	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator>();
		anim.enabled = false;
        WaterGameLogic.Instance.distance = totalDistance;
		WaterGameLogic.Instance.ReadyToPlay = false;
		time = 3f;
		minutes = (int)(time / 60);
		seconds = (int)(time % 60);
	}
	
	// Update is called once per frame
	void Update () {
        if (WaterGameLogic.Instance.ReadyToPlay == false)
		{
			UpdateTimer();
		}
		else
        	WaterGameLogic.Instance.Walk(StepSizeAKASpeed);
	}

	void UpdateTimer()
	{
		time -= Time.deltaTime;
		oldSeconds = seconds;
		minutes = (int)(time / 60);
		seconds = (int)(time % 60);
		
		//instead of updating every frame, update every second change
		if (seconds != oldSeconds)
		{
			if (seconds == 2){
				timer.text = "Ready";
				timer.DOColor(Color.red, .5f).SetLoops(2, LoopType.Yoyo);
			}
			else if (seconds == 1) {
				timer.text = "Set";
				timer.DOColor(Color.yellow, .5f).SetLoops(2, LoopType.Yoyo);
			}
			else if (seconds == 0){
				timer.text = "Go!";
				timer.DOColor(Color.green, .5f).SetLoops(2, LoopType.Yoyo);
			}
			else {
				WaterGameLogic.Instance.ReadyToPlay = true;
				anim.enabled = true;
				timer.text = "";
			}
				
		}
	}
}

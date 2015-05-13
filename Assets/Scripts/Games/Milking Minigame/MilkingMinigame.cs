using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using GoofyGhost.WorldsApart;

public class MilkingMinigame : MonoBehaviour {
    public Text timer;
	public float countDownTime = 3f;
    public float playTime = 20f;
	private float time;
    private int seconds;
    private int minutes;
    private int oldSeconds;
	private bool started;
	private Vector3 originalPos;
	private Vector3 centerPos;
	private CowNipple[] nipples;
	private RectTransform canvas;

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
			minutes = (int)(time / 60);
			seconds = (int)(time % 60);
			timer.rectTransform.position = originalPos;
			for (int i = 0; i < nipples.Length; i++)
				nipples[i].enabled = true;
		}
	}
	void Start() 
	{
		started = false;

		time = countDownTime;
		canvas = (RectTransform)timer.rectTransform.parent;
		originalPos = timer.rectTransform.position;
		centerPos = new Vector3 ((float) -canvas.rect.width * canvas.lossyScale.x, (float)canvas.rect.height * canvas.lossyScale.y, 0f);
		timer.rectTransform.localPosition = centerPos;

		nipples = FindObjectsOfType<CowNipple>();
		for(int i = 0; i < nipples.Length; i++)
			nipples[i].enabled = false;
	}

	void Update () {
		if (time <= 0 && started) 
		{
            this.enabled = false;
			Fader.Instance
				.SetTitle("You have gained +" + "10" + " Food.")
				.FadeOutOnComplete(()=>
				    {
						ThirdWorldManager.Instance.IncrementFood(10);
						Application.LoadLevel("WorldsApart");
					})
				.FadeInOnComplete(()=>ThirdWorldManager.Instance.UsedAction())
				.FadeOutIn();
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
			{
				CountDown();
			}
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
}

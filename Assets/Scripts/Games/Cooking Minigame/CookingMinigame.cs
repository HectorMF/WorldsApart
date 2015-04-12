using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class CookingMinigame : MonoBehaviour {
	private enum State {Starting, Started, Finishing, Finished}
	private State currentState;

	// Game stuff
	public Transform fan;
	public Transform firePit;
	public Text timer;
	public Text temperatureText;

	// Timer stuff
	public float playTime = 45f;
	float time;
	float countDownTime = 3f;
	int minutes;
	int seconds;
	int oldSeconds;
	
	// Input stuff
	Touch touch;
	float oldYPosition;
	float newYPosition;
	float yPositionDiff;
	float maxYPosition = Screen.height;
	float ratioOfScreen;
	float fanRotation;
	float rotationMultiplier = 30;
	float maxRot = 30;
	float minRot = -30;
	bool input;
	
	// Firepit stuff
	float flameTemp = 0;
	SpriteRenderer pitColor;
	int flameDegradeRate = -1;
	bool fanningUp;
	bool changedDirection;
	float fuel = 0;
	float timeCooking = 0;
	Color cold = Color.black;
	Color lowTemp = Color.yellow;
	Color medTemp = new Color(1,120.0f/255.0f, 0, 1);
	Color highTemp = Color.red;
	Color currentColor;
	
	void Start()
	{
		pitColor = firePit.GetComponent<SpriteRenderer>();
		currentState = State.Starting;
		ThirdWorldManager.Instance.DecrementFood(3);
		pitColor.color = cold;
		currentColor = cold;
		//flameDegradeRate = ThirdWorldManager.instance.difficulty;
	}
	void Update () 
	{
		switch (currentState)
		{
		case State.Starting:
			if (countDownTime <= 0) currentState = State.Started;
			else UpdateTimer();
			break;
		case State.Started:
			if (playTime <= 0) currentState = State.Finishing;
			else
			{
				UpdateTimer();
				UpdateInput();
				UpdateFirePit();
			}
			break;
		case State.Finishing:
			Fader.FadeOutIn(Fader.Gesture.None, 0, 2,string.Format("You cooked {0} food!", FoodGain()),"", EndGame); 
			currentState = State.Finished;
			break;
		case State.Finished:
			this.enabled = false;
			Invoke("EndGame", 3);
			break;
		}
	}
	void UpdateTimer()
	{
		if (currentState == State.Starting)
		{
			countDownTime -= Time.deltaTime;
			time = countDownTime;
		}
		else if (currentState == State.Started)
		{
			playTime -= Time.deltaTime;
			time = playTime;
		}
		oldSeconds = seconds;
		minutes = (int)(time / 60);
		seconds = (int)(time % 60);
		
		//instead of updating every frame, update every second change
		if (seconds != oldSeconds)
		{
			if (currentState == State.Starting && seconds == 2) timer.text = "Ready";
			else if (currentState == State.Starting && seconds == 1) timer.text = "Set";
			else if (currentState == State.Starting && seconds == 0) timer.text = "Go!";
			else timer.text = minutes + ":" + seconds.ToString("00");
			
			if (minutes == 0 && seconds <= 10)
			{
				timer.DOColor(Color.red, .5f).SetLoops(2, LoopType.Yoyo);
				timer.gameObject.transform.DOScale(new Vector3(1.5f, 1.5f, 1), .5f).SetLoops(2, LoopType.Yoyo);
			}
		}
		temperatureText.text = Mathf.FloorToInt(flameTemp).ToString();
	}
	void UpdateInput(){
		if(Input.GetMouseButton(0))
		{
			input = true;
			if(Input.GetMouseButtonDown(0)) oldYPosition = Input.mousePosition.y;	
			newYPosition = Input.mousePosition.y;
		}
		else if (Input.touchCount > 0)
		{
			input = true;
			touch = Input.GetTouch(0);
			if (touch.phase == TouchPhase.Began) oldYPosition = touch.position.y;
			else if(touch.phase == TouchPhase.Moved) newYPosition = touch.position.y;
		}
		
		if (input)
		{
			yPositionDiff = newYPosition - oldYPosition;
			ratioOfScreen = yPositionDiff/maxYPosition;
			fanRotation = ratioOfScreen * rotationMultiplier;
			
			if (fan.rotation.z > maxRot) fan.rotation = Quaternion.AngleAxis(maxRot, Vector3.forward);
			else if (fan.rotation.z < minRot) fan.rotation = Quaternion.AngleAxis(minRot, Vector3.forward);
			else fan.Rotate(-Vector3.forward * fanRotation);
			
			oldYPosition = newYPosition;
		}
	}
	void UpdateFirePit(){
		if (flameTemp > 0) flameTemp += flameDegradeRate;
		else flameTemp = 0;

		UpdateStones();

		if((yPositionDiff > 0 && !fanningUp) || (yPositionDiff < 0 && fanningUp))
		{
			fanningUp = !fanningUp;
			changedDirection = true;
		} 
		else fuel += Mathf.Abs(yPositionDiff) * Time.deltaTime;
		
		if (changedDirection)
		{
			changedDirection = false;
			flameTemp += fuel;
			fuel = 0;
		}
		if (flameTemp >= 300 && flameTemp <= 350) timeCooking += Time.deltaTime;
	}
	void UpdateStones()
	{
		Color target;
		if (flameTemp < 150) target = cold;
		else if(flameTemp >= 150 && flameTemp < 300) target = lowTemp;
		else if (flameTemp >= 300 && flameTemp <= 350) target = medTemp;
		else target = highTemp;

		currentColor = Color.Lerp(currentColor, target, Time.deltaTime);
		pitColor.color = currentColor;
	}
	void EndGame()
	{
		ThirdWorldManager.Instance.IncrementFood(FoodGain());
		ThirdWorldManager.Instance.UsedAction();
		Application.LoadLevel("WorldsApart");
	}

	int FoodGain()
	{
		return Mathf.FloorToInt(timeCooking/5);
	}
}

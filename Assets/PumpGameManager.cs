using UnityEngine;
using System.Collections;

public class PumpGameManager : MonoBehaviour 
{
	enum Difficulty { EASY = 2, MEDIUM = 3, HARD = 1 }
	enum Quantity { Perfect = 5, Good = 4, Ok = 3, Bad = 2 } // percentages
	enum State { Starting, Started, Finishing, Finished }

	public Transform pumpMeter;
	public Transform barSlider;
	public Transform waterMeter;
	public float speed = 1f;
	public bool ready = false;
	
	private float yAxisBounds = 1f;
	private float sweetSpot = 0.85f;
	private float waterToAdd;
	private float easyOffset = 0.12f;
	private float mediumOffset = 0.09f;
	private float hardOffset = 0.06f;
	private float gameTime = 20.0f;
	private float maxWater;
	private Difficulty difficulty = Difficulty.EASY;
	private State state = State.Starting;

	void Start (){
		state = State.Starting;
		barSlider.localScale = new Vector3(1f, (float)difficulty, 1f);
		maxWater = ThirdWorldManager.Instance.WaterCapacity;
		ready = false;
	}

	void Update () {
		float diff = 0;
		string timerText = "";
		float seconds = 0;
		float milliSeconds = 0;

		switch (state){
			case State.Starting:
				// Display rules 
				// Count down to start
				if (ready) state = State.Started;
				break;
			case State.Started:
				// Move BarSlider
				barSlider.Translate(Vector3.up * speed * Time.deltaTime);
				// Get input / check bounds
				if (barSlider.position.y >= yAxisBounds || barSlider.position.y <= -yAxisBounds
			    	|| Input.touchCount > 0 || Input.GetMouseButtonDown(0))
				{
					// Switch directions
					speed = - speed;
					// Calculate distance from sweetspot
					diff = Mathf.Abs(barSlider.position.y - sweetSpot);
					// Calculate water gained
					WaterGained(diff); 
				}
				// Decrement game time
				gameTime -= Time.deltaTime;
				//gameTime = Mathf.Abs(gameTime);
				if (gameTime <= 0) { state = State.Finishing; }
				break;
			case State.Finishing:
				// Add water
				// Display How much water they pumped and stats of the game (accuracy, # of consecutive perfect pumps)
				AddWater (Mathf.FloorToInt(waterToAdd));
				state = State.Finished;	
				break;
			case State.Finished:
				// Call Navid's water balancing game
				break;
		}
	}

	void OnGUI()
	{
		string timerText = "00:000";
		float milliSeconds;
		float seconds;
		if (state == State.Started)
		{
			seconds = gameTime % 60;
			milliSeconds = (gameTime % 1) * 1000;
			timerText = string.Format("{0:00}:{1:000}", seconds, milliSeconds);
		}
		GUI.Label(new Rect(275,20,75,25),timerText);
	}
	void WaterGained(float diff)
	{
		switch(difficulty)
		{
			case Difficulty.EASY:
				if(diff <= easyOffset) waterToAdd += ((float)Quantity.Perfect/100) * maxWater;
				else if (diff <= easyOffset * 2) waterToAdd += ((float)Quantity.Good/100) * maxWater;
				else if (diff <= easyOffset * 3) waterToAdd += ((float)Quantity.Ok/100) * maxWater;
				else waterToAdd += ((float)Quantity.Bad/100) * maxWater;
				break;
			case Difficulty.MEDIUM:
				if(diff <= mediumOffset) waterToAdd += ((float)Quantity.Perfect/100) * maxWater;
				else if (diff <= mediumOffset * 2) waterToAdd += ((float)Quantity.Good/100) * maxWater;
				else if (diff <= mediumOffset * 3) waterToAdd += ((float)Quantity.Ok/100) * maxWater;
				else waterToAdd += ((float)Quantity.Bad/100) * maxWater;
				break;
			case Difficulty.HARD:
				if(diff <= hardOffset) waterToAdd += ((float)Quantity.Perfect/100) * maxWater;
				else if (diff <= hardOffset * 2) waterToAdd += ((float)Quantity.Good/100) * maxWater;
				else if (diff <= hardOffset * 3) waterToAdd += ((float)Quantity.Ok/100) * maxWater;
				else waterToAdd += ((float)Quantity.Bad/100) * maxWater;
				break;
		}
	}
	void AddWater(int water){
		Debug.Log("Water Added = " + water);
		ThirdWorldManager.Instance.IncrementWater(water);
	}
}
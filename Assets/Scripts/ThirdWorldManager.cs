using UnityEngine;
using System.Collections;

public class ThirdWorldManager
{
	private static ThirdWorldManager instance;

	const int MoodMax = 4;
	const int WaterMax = 100;
	const int DefaultActions = 5;
	const int WaterCapacity = 5;

	public enum Mood
	{
		Depressed = 3,
		Sad,
		Neutral,
		Happy,
		Ecstatic,
	}

	public enum Weather 
	{
		Rainy,
		Nice,
		Dry,
	}

	private int currentWater, currentFood, actions, availableWater;
	private Mood currentMood = Mood.Neutral;
	private int requiredFood = 4;

	public Mood CurrentMood  	{ get { return currentMood; }  set {} }
	public int CurrentWater 	{ get { return currentWater; }  set {} }
	public int CurrentFood 		{ get { return currentFood; }  set {} }
	public int AvailableWater 	{ get { return availableWater; }  set {} }
	public int RequiredFood 	{ get { return requiredFood; }  set {} }
	public int Actions 			{ get { return actions; }  set {} }

	public ThirdWorldManager()
	{
		UnityEngine.Debug.Log("initializing");
		Reinitialize();
		Report();
	}

	public static ThirdWorldManager Instance
	{
		get 
		{
			if (instance == null)
			{
				instance = new ThirdWorldManager();
			}
			return instance;
		}
	}
	
	private void Reinitialize()
	{
		currentWater = currentFood = 0;
		switch (Random.Range(0, 3))
		{
		case (int)Weather.Rainy:
			availableWater = 15;
			IncrementMood();
			actions = (int)currentMood;
			break; 
		case (int)Weather.Nice:
			availableWater = 10;
			actions = (int)currentMood;
			break;
		case (int)Weather.Dry:
			availableWater = 7;
			DecrementMood();
			actions = (int)currentMood;
			break;
		}
	}

	public bool TryAction()
	{
		actions -= 1;
		return actions > 0;
	}

	public void GetWater()
	{
		if(availableWater > 0)
		{
			UnityEngine.Debug.Log("There's some water!");

			int empty = WaterCapacity - currentWater;
			if (empty < availableWater)
			{
				UnityEngine.Debug.Log("fill up to full");
				currentWater = WaterCapacity;
				availableWater -= empty;
			}
			else 
			{
				UnityEngine.Debug.Log("fill up what you can");
				currentWater += availableWater;
				availableWater = 0;
			}
		}
	}

	public void IncrementWater(int value)
	{
		currentWater = (currentWater + value <= WaterMax) ? currentWater + value : WaterMax;
	}

	public void DecrementWater(int value)
	{
		currentWater -= (currentWater - value > 0) ? value : 0;
	}

	public void IncrementMood()
	{
		switch(currentMood)
		{
		case Mood.Depressed:
			currentMood = Mood.Sad;
			break;
		case Mood.Sad:
			currentMood = Mood.Neutral;
			break;
		case Mood.Happy:
			currentMood = Mood.Ecstatic;
			break;
		case Mood.Ecstatic:
			currentMood = Mood.Ecstatic;
			break;
		}
	}
	
	public void DecrementMood()
	{
		switch(currentMood)
		{
		case Mood.Depressed:
			currentMood = Mood.Depressed;
			break;
		case Mood.Sad:
			currentMood = Mood.Depressed;
			break;
		case Mood.Happy:
			currentMood = Mood.Neutral;
			break;
		case Mood.Ecstatic:
			currentMood = Mood.Happy;
			break;
		}
	}

	public void IncrementFood(int value)
	{
		currentFood += value;
		if (currentFood >= requiredFood) IncrementMood();
	}
	
	public void DecrementFood()
	{
		currentFood -= (currentFood - 1 >= 0) ? 1 : 0;
	}

	public void Report()
	{
		UnityEngine.Debug.Log("Food: " + currentFood);
		UnityEngine.Debug.Log("Mood: " + currentMood);
		UnityEngine.Debug.Log("Water: " + currentWater);
		UnityEngine.Debug.Log("Actions: " + actions);
		UnityEngine.Debug.Log("Available Water: " + availableWater);
		UnityEngine.Debug.Log("-----------------------------------------");
	}
}

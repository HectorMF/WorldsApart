using UnityEngine;
using System.Collections;

public class ThirdWorldManager
{
	const int MoodMax = 4;
	const int WaterMax = 100;
	const int DefaultActions = 5;

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

	public int CurrentWater { get; private set; }
	public int CurrentMood { get; private set; }
	public int CurrentFood { get; private set; }
	public int RequiredFood { get; set; }
	public int Actions { get; private set; }

	public ThirdWorldManager()
	{
		Reinitialize();
	}

	private void Reinitialize()
	{
		switch (Random.Range(0, 3))
		{
		case (int)Weather.Rainy:
			availableWater = 150;
			IncrementMood();
			actions = (int)currentMood;
			break; 
		case (int)Weather.Nice:
			availableWater = 100;
			actions = (int)currentMood;
			break;
		case (int)Weather.Dry:
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
	}
	
	public void DecrementFood()
	{
		currentFood -= (currentFood - 1 >= 0) ? 1 : 0;
	}
}

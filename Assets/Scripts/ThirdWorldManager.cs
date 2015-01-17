using UnityEngine;
using System.Collections;

public class ThirdWorldManager
{
	private static ThirdWorldManager instance;

	const int MoodMax = 4;
	const int DefaultActions = 5;
	const int WaterCapacity = 20;

	public enum Mood
	{
		Depressed = 2,
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
	private Weather currentWeather;
	private int requiredFood = 12;
	public bool ProvidedWaterToFamily;

	public Mood CurrentMood  	{ get { return currentMood; }  set {} }
	public int CurrentWater 	{ get { return currentWater; }  set {} }
	public int CurrentFood 		{ get { return currentFood; }  set {} }
	public int AvailableWater 	{ get { return availableWater; }  set {} }
	public int RequiredFood 	{ get { return requiredFood; }  set {} }
	public int Actions 			{ get { return actions; }  set {} }

	public ThirdWorldManager()
	{
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
		ProvidedWaterToFamily = false;
		currentWater = currentFood = 0;
		float rand = Random.Range(0.0f, 1.0f);
		if (rand < 0.1f)
		{
			currentWeather = Weather.Rainy;
			availableWater = 30;
			IncrementMood();
			actions = (int)currentMood;
		}
		else if (rand < 0.5f)
		{
			currentWeather = Weather.Nice;
			availableWater = 25;
			actions = (int)currentMood;
		}
		else
		{
			currentWeather = Weather.Dry;
			availableWater = 20;
			DecrementMood();
			actions = (int)currentMood;
		}
		UnityEngine.Debug.Log ("A new day! The weather is " + currentWeather);
	}

	public bool TryAction()
	{
		actions -= 1;
		if(actions < 0) 
			return ResolveDay();
		else
			return actions >= 0;
	}

	private bool ResolveDay()
	{
		if (currentFood >= requiredFood) 
		{ 
			Debug.Log ("You fed your family!");
			IncrementMood();
		}
		else
		{
			DecrementMood();
			Debug.Log("Your family is hungry");
		}

		if (ProvidedWaterToFamily)
		{ 
			Debug.Log ("Your family drank enough water today!");
			IncrementMood();
		}
		else
		{
			DecrementMood();
			Debug.Log("Your family is thirsty");
		}

		Report();
		UnityEngine.Debug.ClearDeveloperConsole();
		Reinitialize();
		return false;
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
		currentWater = (currentWater + value <= WaterCapacity) ? currentWater + value : WaterCapacity;
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
		case Mood.Neutral:
			currentMood = Mood.Happy;
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
		case Mood.Neutral:
			currentMood = Mood.Sad;
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

	public void Report()
	{
		string weather = "The weather is " + currentWeather + ". ";
		string food = currentFood > requiredFood ? "You have enough food. " : "You don't have enough food. ";
		string mood = "You're feeling " + currentMood + ".";
		string water = "You've got " + currentWater + "L of water. ";
		string well = "The well has " + availableWater + "L left. ";
		string actions_left = "You can do " + actions + " more things. ";
		UnityEngine.Debug.Log(weather + food + water + well + actions_left + mood);
		UnityEngine.Debug.Log("-----------------------------------------");
	}
}

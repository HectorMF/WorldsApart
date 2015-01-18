﻿using UnityEngine;
using System.Collections;
using WorldsApart.Handlers;

public class ThirdWorldManager 
{
	private static ThirdWorldManager instance;

	public delegate void DayEnd();
	public static event DayEnd OnDayEnd;

	public delegate void NewWeather(Weather newWeather);
	public static event NewWeather OnNewWeather;

    public delegate void HasPackChanged(int value);
    public static event HasPackChanged OnHasPackChanged;

	const int MoodMax = 4;
	const int DefaultActions = 5;
	public int WaterCapacity = 20;

    private bool _hasPack;
    public bool HasPack
    {
        get { return _hasPack; }
        set
        {
            _hasPack = value;
            var handler = new SetAnimationBoolHandler();
            handler.gameObject = GameObject.Find("MainChar");
            handler.booleanName = "hasPack";
            handler.value = value;
            handler.Invoke();
            if (value) WaterCapacity = 30;
            else WaterCapacity = 20;

            if (OnHasPackChanged != null) OnHasPackChanged(WaterCapacity);
        }
    }

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

	private int currentFood, actions, availableWater;
    private int prevWater = 0;
    private int _currentWater;
    private int currentWater
    {
        get { return _currentWater; }
        set
        {
            _currentWater = value;
            if ((prevWater == 0 && _currentWater > 0) || (prevWater > 0 && _currentWater == 0))
            {
                var handler = new SetAnimationBoolHandler();
                handler.gameObject = GameObject.Find("MainChar");
                handler.booleanName = "hasWater";
                handler.value = _currentWater > 0;
                handler.Invoke();
            }
        }
    }
	private Mood currentMood = Mood.Neutral;
	private Weather currentWeather;
	private int requiredFood = 12;
	public bool ProvidedWaterToFamily;

	public Weather CurrentWeather { get { return currentWeather; } set {} }
	public Mood CurrentMood  	{ get { return currentMood; }  set {} }
	public int CurrentWater 	{ get { return currentWater; }  set {} }
	public int CurrentFood 		{ get { return currentFood; }  set {} }
	public int AvailableWater 	{ get { return availableWater; }  set {} }
	public int RequiredFood 	{ get { return requiredFood; }  set { requiredFood = value; } }
	public int Actions 			{ get { return actions; }  set {} }
	public bool AnyWater		{ get { return currentWater > 0; } set {} }

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
		else if (rand < 0.7f)
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

		Debug.Log(OnNewWeather != null);
		if (OnNewWeather != null) OnNewWeather(currentWeather);

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

		Report();
		if(OnDayEnd != null) OnDayEnd();
		Reinitialize();
		return false;
	}
	
	public void GetWater()
	{
		if(availableWater > 0)
		{
			UnityEngine.Debug.Log("There's some water!");

			int empty = WaterCapacity - currentWater;
			if (empty == 0) return;
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

	// Returns amount of water used from 
	public int DecrementWater(int attemptedValue)
	{
		if (currentWater - attemptedValue > 0)
		{
			currentWater -= attemptedValue;
			return attemptedValue;
		}
		else
		{
			int used = currentWater;
			currentWater = 0;
			return used;
		}
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

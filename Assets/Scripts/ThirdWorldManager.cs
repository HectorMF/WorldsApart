using UnityEngine;
using System.Collections;
using WorldsApart.Handlers;
using Parse;
using System.Collections.Generic;


public class ThirdWorldManager 
{

    public enum Weather
    {
        Rainy,
        Nice,
        Dry,
    }
	public delegate void DayEnd();
	public static event DayEnd OnDayEnd;

	public delegate void NewWeather(Weather newWeather);
	public static event NewWeather OnNewWeather;

    public delegate void HasPackChanged(int value);
    public static event HasPackChanged OnHasPackChanged;

    private Mood mood = new Mood();
    private Weather currentWeather;
    private int requiredFood;
    private int currentFood, actions, availableWater;
    private int prevWater = 0;
    private int _currentWater;
    private int _daysAlive;

    #region Properties
    public int WaterCapacity = 20;
    public Weather CurrentWeather { get { return currentWeather; } set { } }
    public Mood.MoodNames CurrentMood { get { return mood.CurrentMood; } set { } }
    public int CurrentWater { get { return currentWater; } }
    public int CurrentFood { get { return currentFood; }}
    public int AvailableWater { get { return availableWater; } set { } }
    public int RequiredFood { get { return requiredFood; } set { requiredFood = value; } }
    public int Actions { get { return actions; } set { } }
    public bool AnyWater { get { return currentWater > 0; } set { } }

    private int currentWater
    {
        get { return _currentWater; }
        set
        {
            prevWater = _currentWater;
            _currentWater = value;
            if (ShouldSwapSprite()) InvokeSpriteSwap();
        }
    }

    #endregion

    #region SingeltonStuff
    private static ThirdWorldManager instance;
    private ThirdWorldManager()
    {
        //LoadData();
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
    #endregion endOfSingeltonStuff

    #region PrivateMethods
    private void Reinitialize()
    {
        
        float rand = Random.Range(0.0f, 1.0f);
        if (rand < 0.1f)
        {
			WeatherManager.weather = global::Weather.Rain;
            currentWeather = Weather.Rainy;
            availableWater = Random.Range(35, 45);
            IncrementMood();
            actions = (int)CurrentMood;
        }
        else if (rand < 0.7f)
        {
			WeatherManager.weather = global::Weather.None;
            currentWeather = Weather.Nice;
            availableWater = Random.Range(25, 35);
            actions = (int)CurrentMood;
        }
        else
        {
			WeatherManager.weather = global::Weather.Dry;
            currentWeather = Weather.Dry;
            availableWater = Random.Range(15, 25);
            DecrementMood();
            actions = (int)CurrentMood;
        }

        if (OnNewWeather != null) OnNewWeather(currentWeather);

        UnityEngine.Debug.Log("A new day! The weather is " + currentWeather);
    }
    private void ResolveDay()
    {
        if (currentFood >= requiredFood)
        {
            Debug.Log("You fed your family!");
            IncrementMood();
        }
        else
        {
            DecrementMood();
            Debug.Log("Your family is hungry");
        }

        Report();
        if (OnDayEnd != null) OnDayEnd();
        Reinitialize();
    }

    private bool LoadData()
    {
        ParseObject result = new ParseObject("NullObject");
        ParseQuery<ParseObject> query = ParseObject.GetQuery("SaveObject").WhereEqualTo("Device ID", SystemInfo.deviceUniqueIdentifier);
        query.FirstAsync().ContinueWith(t =>
        {
            result = t.Result;
        });
        if(result.ClassName == "SaveObject")
        {
            _currentWater = result.Get<int>("Water");
            currentFood = result.Get<int>("Food");
            CurrentMood = (Mood.MoodNames)result.Get<int>("Mood");
            _daysAlive = result.Get<int>("Days Alive");
            _hasPack = result.Get<bool>("Has Pack");
            return true;
        }
        currentWater = currentFood = 0;
        return false;
    }


    bool ShouldSwapSprite()
    {
        return (prevWater == 0 && _currentWater > 0) || (prevWater > 0 && _currentWater == 0);
    }

    void InvokeSpriteSwap()
    {
        var handler = new SetAnimationBoolHandler();
        handler.gameObject = GameObject.Find("MainChar");
        handler.booleanName = "hasWater";
        handler.value = _currentWater > 0;
        handler.Invoke();
    }
    #endregion
    

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

	public void GetWater()
	{
		if(availableWater > 0)
		{
			int empty = WaterCapacity - currentWater;
			if (empty == 0) return;
			if (empty < availableWater)
			{
				currentWater = WaterCapacity;
				availableWater -= empty;
			}
			else 
			{
				currentWater += availableWater;
				availableWater = 0;
			}
		}
	}

	public void IncrementWater(int value)
	{
		currentWater = (currentWater + value <= WaterCapacity) ? currentWater + value : WaterCapacity;
	}

	// Returns amount of water used 
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

	public bool IsRaining()
	{
		return currentWeather == Weather.Rainy;
	}
	
	public void UsedAction()
	{
		actions -= 1;
		if(actions <= 0) ResolveDay();
	}

	public void IncrementMood()
	{
		mood.IncrementMood();
	}
	
	public void DecrementMood()
	{
		mood.DecrementMood();
	}

	public void IncrementFood(int value)
	{
		currentFood += value;
	}

    public int DecrementFood(int attemptedValue)
    {
        if (currentFood - attemptedValue > 0)
        {
            currentFood -= attemptedValue;
            return 0;
        }
        else
        {
            int need = currentFood - attemptedValue;
            currentFood = 0;
            return need;
        }
    }

	public void Report()
	{
		string weather = "The weather is " + currentWeather + ". ";
		string food = currentFood > requiredFood ? "You have enough food. " : "You don't have enough food. ";
		string mood = "You're feeling " + CurrentMood + ".";
		string water = "You've got " + currentWater + "L of water. ";
		string well = "The well has " + availableWater + "L left. ";
		string actions_left = "You can do " + actions + " more things. ";
		Debug.Log(weather + food + water + well + actions_left + mood);
		Debug.Log("-----------------------------------------");
	}

}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TroughManager : MonoBehaviour {

	public List<AnimalThirst> animalThirsts;
	public int TotalWaterRequired;
	public int CurrentWater;
	bool firstRun = true;

	public delegate void ResolveWater();
	public static event ResolveWater OnResolveWater;

	public delegate void WaterAvailable();
	public static event ResolveWater OnWaterAvailable;

	void OnEnable()
	{
		ThirdWorldManager.OnNewWeather += ResetWaterRequirement;
		ThirdWorldManager.OnDayEnd += DistributeWater;
	}
	
	void OnDisable()
	{
		ThirdWorldManager.OnNewWeather -= ResetWaterRequirement;
		ThirdWorldManager.OnDayEnd -= DistributeWater;
	}
	
	public void DistributeWater()
	{
		// actually distribute water
		// go through children, and allocate water to each
		foreach(AnimalThirst a in animalThirsts)
		{
			a.Drink();
		}
		if (OnResolveWater != null) OnResolveWater();
	}

	// Returns amount of water used 
	public int DecrementWater(int attemptedValue)
	{
		if (CurrentWater - attemptedValue > 0)
		{
			CurrentWater -= attemptedValue;
			return attemptedValue;
		}
		else
		{
			int used = CurrentWater;
			CurrentWater = 0;
			return used;
		}
	}
	
	void ResetWaterRequirement(ThirdWorldManager.Weather newWeather)
	{
		SetDailyWaterRequirement();
	}

	void Update()
	{
		if(firstRun)
		{
			foreach(AnimalThirst t in GetComponentsInChildren<AnimalThirst>()) 
				animalThirsts.Add(t);
			firstRun = false;
			SetDailyWaterRequirement();
		}
	}

	void SetDailyWaterRequirement() {
		CurrentWater = 0;
		TotalWaterRequired = 0;
		foreach(Thirst t in animalThirsts) {
			TotalWaterRequired += t.TotalRequiredWater;
		}
		Debug.Log(string.Format("========== Animals need {0} water ==========", TotalWaterRequired));
	}

	void ShuffleThirsts()
	{
		int n = animalThirsts.Count;
		while (n > 1) {
			n--;
			int k = Random.Range(0, n + 1);
			AnimalThirst value = animalThirsts[k];
			animalThirsts[k] = animalThirsts[n];
			animalThirsts[n] = value;
		}
	}

	public void AddWaterToTrough()
	{
		CurrentWater = ThirdWorldManager.Instance.DecrementWater(TotalWaterRequired - CurrentWater);
		if(OnWaterAvailable != null) OnWaterAvailable();
	}

	public bool NeedsWater()
	{
		return CurrentWater < TotalWaterRequired;
	}
}

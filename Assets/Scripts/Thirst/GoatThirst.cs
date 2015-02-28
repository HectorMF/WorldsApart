using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GoatThirst : Thirst {

	void Start () 
	{
		InitializeWaterRequirements(new ThirstInfo().Goat());
	}

	public override void DrinkSuccess ()
	{
		base.DrinkSuccess();
		ThirdWorldManager.Instance.IncrementFood(Random.Range(1, 3));
	}

	public override void DayEnd ()
	{
		base.DayEnd ();
		if(ThirdWorldManager.Instance.CurrentWeather == ThirdWorldManager.Weather.Rainy)
		{
			if (Random.Range(0.0f, 1.0f) < 0.25f) 
			{
				MemberCount = MemberCount - 1;
				Debug.Log("LIGHTING STRIKES!!!!!!! OH THE HUMANITY!!");
			}
		}
	}
}
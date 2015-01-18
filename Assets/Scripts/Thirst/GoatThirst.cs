using UnityEngine;
using System.Collections;

public class GoatThirst : Thirst {

	void Start () 
	{
		SurvivesFor = 2;
		WaterRequiredPerMember = 2;
		MemberCount = 1;
		DaysWithoutWater = 0;
	}

	public override void DrinkSuccess ()
	{
		base.DrinkSuccess();
		ThirdWorldManager.Instance.IncrementFood(2 * MemberCount);
		Debug.Log("Your " + name + " has enough water");
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
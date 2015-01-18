using UnityEngine;
using System.Collections;

public class CropsThirst : Thirst {
		
	void Start () {
		SurvivesFor = 1;
		WaterRequiredPerMember = 1;
		MemberCount = 10;
		DaysWithoutWater = 0;
	}
	
	public override void DrinkSuccess() {
		base.DrinkSuccess();
		ThirdWorldManager.Instance.IncrementFood(MemberCount);
		Debug.Log("Your " + name + " has enough water");
	}

	public override void GetTheWeather(ThirdWorldManager.Weather newWeather) {
		if (newWeather == ThirdWorldManager.Weather.Rainy) {
			WaterRequiredPerMember = 0;
			DaysWithoutWater = 0;
			Debug.Log("it's raining");
			// This crashes unity on Ryan Bone's laptop
//			ThirdWorldManager.Instance.IncrementFood(MemberCount);
		}
	}

	public override void DayEnd() {
		base.DayEnd ();
		WaterRequiredPerMember = 1;
	}
}
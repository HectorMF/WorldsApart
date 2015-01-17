using UnityEngine;
using System.Collections;

public class FamilyThirst : Thirst {

	void Start () {
		SurvivesFor = 3;
		WaterRequiredPerMember = 2;
		MemberCount = 4;
		DaysWithoutWater = 0;
	}
	
	public override void DrinkSuccess()
	{
		ThirdWorldManager.Instance.IncrementMood();
		ThirdWorldManager.Instance.ProvidedWaterToFamily = true;
		Debug.Log("Your " + name + " has enough water");
	}
}

//public int DaysWithoutWater { get; set; }
//public int SurvivesFor { get; private set; }
//public int WaterRequiredPerMember { get; } 
//public int MemberCount { get; set; }
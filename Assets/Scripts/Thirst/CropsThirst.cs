using UnityEngine;
using System.Collections;

public class CropsThirst : Thirst {
		
	void Start () {
		SurvivesFor = 1;
		WaterRequiredPerMember = 1;
		MemberCount = 10;
		DaysWithoutWater = 0;
	}
	
	public override void DrinkSuccess()
	{
		ThirdWorldManager.Instance.IncrementFood(MemberCount);
		Debug.Log("Your " + name + " has enough water");
	}
}
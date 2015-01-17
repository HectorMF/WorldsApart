using UnityEngine;
using System.Collections;

public class ChickenThirst : Thirst {
	
	void Start () {
		SurvivesFor = 1;
		WaterRequiredPerMember = 1;
		MemberCount = 3;
		DaysWithoutWater = 0;
	}
	
	public override void DrinkSuccess()
	{
		base.DrinkSuccess();
		ThirdWorldManager.Instance.IncrementFood(MemberCount);
		Debug.Log("Your " + name + " has enough water");
	}
}
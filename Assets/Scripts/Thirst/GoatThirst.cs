using UnityEngine;
using System.Collections;

public class GoatThirst : Thirst {

	void Start () 
	{
		SurvivesFor = 2;
		WaterRequiredPerMember = 2;
		MemberCount = 2;
		DaysWithoutWater = 0;
	}

	public override void DrinkSuccess ()
	{
		base.DrinkSuccess();
		ThirdWorldManager.Instance.IncrementFood(2 * MemberCount);
		Debug.Log("Your " + name + " has enough water");
	}
}
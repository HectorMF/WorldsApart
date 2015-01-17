using UnityEngine;
using System.Collections;

public class FamilyThirst : Thirst {

	int foodRequiredPerPerson = 3;

	void Start () {
		SurvivesFor = 3;
		WaterRequiredPerMember = 2;
		MemberCount = 4;
		DaysWithoutWater = 0;
		SetFoodRequirements();
	}

	void SetFoodRequirements()
	{
		ThirdWorldManager.Instance.RequiredFood = foodRequiredPerPerson * MemberCount;
	}
	
	public override void DrinkSuccess()
	{
		ThirdWorldManager.Instance.IncrementMood();
		ThirdWorldManager.Instance.ProvidedWaterToFamily = true;
		Debug.Log("Your " + name + " has enough water");
	}

	public override void DayEnd ()
	{
		if (AmountDrank >= TotalRequiredWater)
		{ 
			Debug.Log ("Your family drank enough water today!");
		}
		else
		{
			ThirdWorldManager.Instance.DecrementMood();
			string adj;
			if (AmountDrank >= 0 && AmountDrank < 3)
				adj = " extremely ";
			else if (AmountDrank >= 4 && AmountDrank < 7)
				adj = " very ";
			else
				adj = " somewhat ";

			Debug.Log("Your family is" + adj + "thirsty");
		}
		SetFoodRequirements();
		base.DayEnd ();
	}
}

//public int DaysWithoutWater { get; set; }
//public int SurvivesFor { get; private set; }
//public int WaterRequiredPerMember { get; } 
//public int MemberCount { get; set; }
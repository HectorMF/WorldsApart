using UnityEngine;
using System.Collections;

public class FamilyThirst : Thirst {

	int foodRequiredPerPerson = 3;

	void Start () 
	{
		InitializeWaterRequirements(new ThirstInfo().Family());
		SetFoodRequirements();
	}

	void SetFoodRequirements()
	{
		ThirdWorldManager.Instance.RequiredFood = foodRequiredPerPerson * MemberCount;
	}
	
	public override void DrinkSuccess()
	{
		base.DrinkSuccess();
		ThirdWorldManager.Instance.IncrementMood();
	}

	public override void DayEnd ()
	{
		ReportFoodUsage ();
		ReportWaterUsage ();
		base.DayEnd ();
		SetFoodRequirements();
	}

	void ReportFoodUsage ()
	{

		if (ThirdWorldManager.Instance.CurrentFood < MemberCount * foodRequiredPerPerson) {
			ThirdWorldManager.Instance.CurrentFood = 0;
			Debug.Log("Your family did not eat enough today");
		} else {
			ThirdWorldManager.Instance.CurrentFood = ThirdWorldManager.Instance.CurrentFood - (MemberCount * foodRequiredPerPerson);
			Debug.Log("Your family ate enough today");
		}
	}

	void ReportWaterUsage ()
	{
		if (AmountDrank >= TotalRequiredWater) {
			Debug.Log ("Your family drank enough water today!");
		}
		else {
			ThirdWorldManager.Instance.DecrementMood ();
			string adj;
			if (AmountDrank >= 0 && AmountDrank < 4)
				adj = "extremely";
			else
				if (AmountDrank >= 4 && AmountDrank < 7)
					adj = "very";
				else
					adj = "somewhat";
			Debug.Log (string.Format ("Your family is {0} thirsty", adj));
		}
	}
}
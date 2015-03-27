using UnityEngine;
using System.Collections;

public class AnimalThirst : Thirst {

	public void Drink()
	{
		if(NoWaterRequirementOnRainyDay())
		{
			DrinkSuccess();
		}
		else 
		{
			DrinkOnNormalday();
		}
	}

	private void DrinkOnNormalday()
	{
		Debug.Log("animal drinking");
		AmountDrank += gameObject.GetComponentInParent<TroughManager>().DecrementWater(TotalRequiredWater - AmountDrank);
		if (AmountDrank >= TotalRequiredWater)
		{
			DrinkSuccess();
		}
		Debug.Log (string.Format("Name: {2}, Tot Req H20: {1}, Drank: {3}", MemberCount, TotalRequiredWater, name, AmountDrank));
	}
}
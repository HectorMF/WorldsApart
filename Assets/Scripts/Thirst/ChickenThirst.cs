using UnityEngine;
using System.Collections;

public class ChickenThirst : AnimalThirst {
	
	void Start ()
	{
		InitializeWaterRequirements(new ThirstInfo().Chicken());
	}
	
	public override void DrinkSuccess()
	{
		base.DrinkSuccess();
		ThirdWorldManager.Instance.IncrementFood(Random.Range(1,2));
	}
}
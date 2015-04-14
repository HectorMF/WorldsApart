using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CowThirst : Thirst {
	
	void Start () 
	{
		InitializeWaterRequirements(new ThirstInfo().Cow());
	}
	
	public override void DrinkSuccess ()
	{
		base.DrinkSuccess();
		ThirdWorldManager.Instance.IncrementFood(Random.Range(2, 3));
	}
}
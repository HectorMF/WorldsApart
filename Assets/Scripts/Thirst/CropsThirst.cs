using UnityEngine;
using System.Collections;

public class CropsThirst : Thirst {
		
	void Start () 
	{
		InitializeWaterRequirements(new ThirstInfo().Crops());
	}
	
	public override void DrinkSuccess()
	{
		base.DrinkSuccess();
		ThirdWorldManager.Instance.IncrementFood(Random.Range ((int)MemberCount/2, (int)(3/2) * MemberCount));
	}
}
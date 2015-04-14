using UnityEngine;
using System.Collections;

public class FirePitThirst : Thirst {

	// Use this for initialization
	void Start ()
	{
		InitializeWaterRequirements(new ThirstInfo().FirePit());
	}
	
	public override void DrinkSuccess()
	{
		// Firepits don't really drink, but they need this script to make 
		// its info popup play nicely and enforce a water requirement
	}
}

using UnityEngine;
using System.Collections;

using WorldsApart.Utility;

public class GetWater : MonoBehaviour {

	void OnEnable()
	{
		TroughManager.OnWaterAvailable += GoGetWater;
	}
	
	void OnDisable()
	{
		TroughManager.OnWaterAvailable -= GoGetWater;
	}

	public void GoGetWater()
	{
		gameObject.GetComponent<Wander>().SetTarget(transform.parent.position);
	}
}
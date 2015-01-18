using UnityEngine;
using System.Collections;

public class PumpStatusNotifier : MonoBehaviour {

	GameObject available, empty;
	// Use this for initialization
	void Start () {
		available = transform.Find("PumpAvailable").gameObject;
		available.SetActive(true);
	}

	void OnEnable()
	{
		ThirdWorldManager.OnDayEnd += DayEnd;
	}
	
	void OnDisable()
	{
		ThirdWorldManager.OnDayEnd -= DayEnd;
	}

	void Update()
	{
		if (ThirdWorldManager.Instance.AvailableWater == 0)
		{
			available.SetActive(false);
		}
	}

	void DayEnd()
	{
		available.SetActive(true);
	}
}

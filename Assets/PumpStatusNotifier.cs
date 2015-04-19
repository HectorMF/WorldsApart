using UnityEngine;
using System.Collections;

public class PumpStatusNotifier : MonoBehaviour {

	public GameObject actionIndicator;

	void Start () 
	{
		actionIndicator = transform.Find("InfoPopUp/Action").gameObject;
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
			actionIndicator.SetActive(false);
			actionIndicator.transform.parent.gameObject.SetActive(false);
		}
	}

	void DayEnd()
	{
		actionIndicator.SetActive(true);
	}
}

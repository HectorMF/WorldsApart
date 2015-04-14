using UnityEngine;
using System.Collections;

public class PumpStatusNotifier : MonoBehaviour {

	public GameObject actionIndicator, Info;

	void Start () 
	{
		actionIndicator = transform.Find("InfoPopUp/Action").gameObject;
		Info = transform.Find ("InfoPopUp/Info").gameObject;
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
		if (Info.activeSelf)
			actionIndicator.SetActive (false);
		else
			actionIndicator.SetActive (true);

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

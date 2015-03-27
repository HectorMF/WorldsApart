using UnityEngine;
using System.Collections;

public class PumpStatusNotifier : MonoBehaviour {

	GameObject status, empty, popup;
	bool turnedOff;
	// Use this for initialization
	void Start () {
		status = transform.Find("PumpStatus").gameObject;
		status.SetActive(true);
		popup = transform.Find("InfoPopUp/PopUpBubble").gameObject;
		turnedOff = false;
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
		if(popup.activeSelf)
			status.SetActive(false);
		else
			status.SetActive(true);

		if (ThirdWorldManager.Instance.AvailableWater == 0 && !turnedOff)
		{
			status.SetActive(false);
			popup.transform.parent.gameObject.SetActive(false);
			turnedOff = true;
		}
	}

	void DayEnd()
	{
		status.SetActive(true);
		popup.transform.parent.gameObject.SetActive(true);
		turnedOff = false;
	}
}

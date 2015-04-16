using UnityEngine;
using System.Collections;

public class SetupOnDayEnd : MonoBehaviour {

	GameObject Info, Action;
	// Use this for initialization
	void Start () {
		Info = transform.Find ("Info").gameObject;
		Action = transform.Find ("Action").gameObject;
	}
	
	void OnEnable()
	{
		ThirdWorldManager.OnDayEnd += DayEnd;
	}
	
	void OnDisable()
	{
		ThirdWorldManager.OnDayEnd -= DayEnd;
	}

	public virtual void DayEnd()
	{
		Info.SetActive (false);
		Action.SetActive (true);
	}
}

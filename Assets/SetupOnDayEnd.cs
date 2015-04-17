using UnityEngine;
using System.Collections;

public class SetupOnDayEnd : MonoBehaviour {

	GameObject Info, Action;
	BoxCollider boxCollider;

	// Use this for initialization
	void Start () {
		boxCollider = GetComponent<BoxCollider> ();
		Info = transform.Find ("Info").gameObject;
		Action = transform.Find ("Action").gameObject;
	}

	public void PlayGame()
	{
		boxCollider.enabled = false;
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
		boxCollider.enabled = true;
	}
}

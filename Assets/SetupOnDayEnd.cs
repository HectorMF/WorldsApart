using UnityEngine;
using System.Collections;
using DG.Tweening;
public class SetupOnDayEnd : MonoBehaviour {

	GameObject Info, Action;
	BoxCollider boxCollider;

	// Use this for initialization
	void Start () {
		boxCollider = GetComponent<BoxCollider> ();
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
		Action.SetActive (true);
		Action.transform.DOScale(Vector3.one, 1f);
		boxCollider.enabled = true;
	}
}

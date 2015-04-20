using UnityEngine;
using System.Collections;
using DG.Tweening;
using WorldsApart.GUI;


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
	

}

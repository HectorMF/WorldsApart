using UnityEngine;
using System.Collections;

public class PumpAnimator : MonoBehaviour {

	Transform idle, pumping;
	float time, totalTime;
	bool IsPumping;
	// Use this for initialization

	void Start () {
		idle = transform.Find("PumpIdle");
		pumping = transform.Find("Pumping");
		totalTime = 1f;
		IsPumping = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (IsPumping)
		{
			time += Time.deltaTime;
		}
		if (time >= totalTime)
		{
			IsPumping = false;
			time = 0;
			idle.gameObject.SetActive(true);
			pumping.gameObject.SetActive(false);
		}
	}

	public void Pump() {
		IsPumping = true;
		idle.gameObject.SetActive(false);
		pumping.gameObject.SetActive(true);
	}
}

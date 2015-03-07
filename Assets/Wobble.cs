using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Wobble : MonoBehaviour {
	float duration = 1;
	float timeBetweenShakes = 4;
	private float timer;
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if(timer >= timeBetweenShakes){
			timer -= timeBetweenShakes;
			transform.DOShakeScale(duration,.2f,4);
			//transform.DOShakePosition(duration,.1f,4);
		}

	}
}

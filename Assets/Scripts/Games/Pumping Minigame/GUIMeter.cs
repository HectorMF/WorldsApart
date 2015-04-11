using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIMeter : MonoBehaviour 
{
	public float speed = 0.1f;

	private int difficulty;
	private float oldValue;
	private float currentValue;
	private Slider slider;
	private bool draining;
	private float drainSpeed;
	private PumpAnimator pumpAnim;
	
	void Start () 
	{
		slider = this.GetComponent<Slider>();
		if (slider != null){
			slider.maxValue = 3;
			slider.value = slider.minValue;
		}
		currentValue = slider.value;
		oldValue = slider.value;
		difficulty = 1;
		draining = false;
		drainSpeed = 2 * -speed;
		pumpAnim = GetComponentInChildren<PumpAnimator>();

	}

	void Update () 
	{
		if(slider.value == slider.maxValue || slider.value == slider.minValue)
			speed = -speed;

		if (!draining && (Input.touchCount > 0 || Input.GetMouseButtonDown(0)))
		{
			draining = true;
			pumpAnim.Pump();
			ThirdWorldManager.Instance.IncrementWater(Mathf.FloorToInt(slider.value));
		}
		else if (draining)
		{
			speed = Mathf.Abs(speed);
			slider.value += drainSpeed * Time.deltaTime;
			if (slider.value == slider.minValue) draining = false;
		}
		else 
			slider.value += speed * Time.deltaTime * difficulty;

	}
}

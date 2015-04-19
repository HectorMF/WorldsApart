using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIMeter : MonoBehaviour 
{
	public Transform pump;
	public float speed = 0.1f;


	int difficulty;
	int waterPumped;
	float oldValue;
	float currentValue;
	Slider slider;
	bool draining;
	float drainSpeed;
	PumpAnimator pumpAnim;
	Color dirtyWater = new Color(182f/255f, 145f/255f, 49f/255f, 1f);
	float chanceDirtyWater = 2;
	int startingWater;
	SpriteRenderer pumpRenderer;
	
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
		pumpAnim = GameObject.Find("Pump").GetComponent<PumpAnimator>();
		                          
		startingWater = ThirdWorldManager.Instance.CurrentWater;
		pumpRenderer = pump.GetComponent<SpriteRenderer>();

	}

	void Update () 
	{
		int rand;
		if(slider.value == slider.maxValue || slider.value == slider.minValue)
			speed = -speed;

		if (!draining && (Input.touchCount > 0 || Input.GetMouseButtonDown(0)))
		{
			draining = true;
			pumpAnim.Pump();
			rand = Random.Range(1, 101);
			if (rand <= chanceDirtyWater)
			{
				pumpRenderer.color = dirtyWater;
				waterPumped = 0;
				ThirdWorldManager.Instance.DecrementWater(ThirdWorldManager.Instance.CurrentWater - startingWater);
			}
			else{
				pumpRenderer.color = Color.white;
				waterPumped += Mathf.FloorToInt(slider.value);
				ThirdWorldManager.Instance.IncrementWater(Mathf.FloorToInt(slider.value));
			}
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
	public int GetWaterPumped()
	{
		return waterPumped;
	}
}

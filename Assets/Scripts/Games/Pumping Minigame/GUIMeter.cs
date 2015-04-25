using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class GUIMeter : MonoBehaviour 
{
	public Transform pump;
	public float speed = 0.1f;
	public Text dirtyWaterText;

	int startingWater;
	int difficulty;
	int waterPumped;
	float oldValue;
	float currentValue;
	float drainSpeed;
	float chanceDirtyWater = 2;
	Slider slider;
	bool draining;
	PumpAnimator pumpAnim;
	Color dirtyWater = new Color(182f/255f, 145f/255f, 49f/255f, 1f);
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
				DirtyWater();
			}
			else{
				waterPumped += Mathf.FloorToInt(slider.value);
				WaterGameLogic.Instance.IncrementWater(Mathf.FloorToInt(slider.value));
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
	void DirtyWater() {
		pumpRenderer.color = dirtyWater;
		WaterGameLogic.Instance.DecrementWater(waterPumped);
		waterPumped = 0;
		dirtyWaterText.text = "Dirty Water!";
		dirtyWaterText.color = dirtyWater;
		dirtyWaterText.transform.DOShakePosition(1,16);
		dirtyWaterText.DOFade(0,.5f).SetDelay(.5f).OnComplete(clearText);
	}
	void clearText()
	{
		pumpRenderer.color = Color.white;
		dirtyWaterText.text = "";
		dirtyWaterText.DOFade(1,0);
	}
}

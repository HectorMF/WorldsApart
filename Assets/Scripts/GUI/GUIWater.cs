using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIWater : MonoBehaviour {
    public float easeTime = 1;

    private float oldValue;
    private float currentValue;
    private Slider slider;
    private float easeTimer;
    private bool easing;
	// Use this for initialization
	void Start () {
        slider = this.GetComponent<Slider>();
        if(slider != null)
            slider.maxValue = 20;
        currentValue = slider.value;
        oldValue = slider.value;
        easing = false;

        ThirdWorldManager.OnHasPackChanged += (value) => slider.maxValue = value;
	}
	
	// Update is called once per frame
	void Update () {
        if (!easing && slider.value != ThirdWorldManager.Instance.CurrentWater)
        {
            oldValue = slider.value;
            currentValue = ThirdWorldManager.Instance.CurrentWater;
            easeTimer = 0;
            easing = true;
        }

        if (easing)
        {
            easeTimer += Time.deltaTime / easeTime;
            if (easeTimer > 1)
            {
                easing = false;
                slider.value = ThirdWorldManager.Instance.CurrentWater;
            }
            else
                slider.value = Mathf.Lerp(oldValue, currentValue, easeTimer);
        }
	}
}

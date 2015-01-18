using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIWater : MonoBehaviour {

    private float oldValue;
    private float currentValue;
    private Slider slider;

	// Use this for initialization
	void Start () {
        slider = this.GetComponent<Slider>();
        if(slider != null)
            slider.maxValue = ThirdWorldManager.Instance.AvailableWater;
	}
	
	// Update is called once per frame
	void Update () {
        if (currentValue != ThirdWorldManager.Instance.CurrentWater)
        {
            slider.maxValue = ThirdWorldManager.Instance.AvailableWater;
        }
	}
}

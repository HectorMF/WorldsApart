using UnityEngine;
using System.Collections;
using DG.Tweening;

public class NippleFiller : MonoBehaviour {
    public static float minValue = 0;
    public static float maxValue = 1;

    public float currentValue;
    public float fillRate = .1f;
    public Vector3 minScale;
    public Vector3 maxScale;

	void Start () {
        currentValue = Random.Range(minValue, maxValue);
	}
	
	void Update () {
        if(currentValue == maxValue) return;

        currentValue += fillRate * Time.deltaTime;
        currentValue = clamp(currentValue, minValue, maxValue);
        transform.localScale = Vector3.Lerp(minScale, maxScale, (currentValue - minValue) / (maxValue - minValue));
        if (currentValue == maxValue)
            transform.DOShakeScale(.5f, new Vector3(.12f, 0, 0),16);
	}

    private float clamp(float val, float min, float max)
    {
        if (val < min)
            return min;
        if (val > max)
            return max;
        return val;
    }
}

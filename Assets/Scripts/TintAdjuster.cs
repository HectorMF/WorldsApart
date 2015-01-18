using UnityEngine;
using System.Collections;
using System;

public class TintAdjuster : MonoBehaviour {
    public float percentTint = .5f;
    private SpriteRenderer renderer;
    
	void Start () {
        renderer = gameObject.GetComponent<SpriteRenderer>();
	}

	void Update () {
        try
        {
            renderer.color = Color.Lerp(Color.white, DayNightController.Instance.camera.backgroundColor, percentTint);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
	}
}

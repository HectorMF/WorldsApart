using UnityEngine;
using System.Collections;

public class TintAdjuster : MonoBehaviour {
    public float percentTint = .5f;
    private SpriteRenderer renderer;
    
	void Start () {
        renderer = gameObject.GetComponent<SpriteRenderer>();
	}

	void Update () {
        renderer.color = Color.Lerp(Color.white, DayNightController.Instance.camera.backgroundColor, percentTint);
	}
}

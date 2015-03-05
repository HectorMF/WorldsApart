using UnityEngine;
using System.Collections;

public class PlayerWalking : MonoBehaviour {
    public float StepSizeAKASpeed = 1f * Time.deltaTime;
    public float totalDistance = 100f;
    private WaterGameLogic wg = WaterGameLogic.Instance;
	// Use this for initialization
	void Start () {
        wg.distance = totalDistance;
	}
	
	// Update is called once per frame
	void Update () {
        wg.Walk(StepSizeAKASpeed * wg.water);
	}
}

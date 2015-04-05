using UnityEngine;
using System.Collections;

public class PlayerWalking : MonoBehaviour {
    public float StepSizeAKASpeed = 1f;
    public float totalDistance = 100f;
    private WaterGameLogic wg;
	// Use this for initialization
	void Start () {
        wg = WaterGameLogic.Instance;
        wg.distance = totalDistance;
        
	}
	
	// Update is called once per frame
	void Update () {
        wg.Walk(StepSizeAKASpeed);
	}
}

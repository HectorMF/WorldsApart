using UnityEngine;
using System.Collections;

public class PlayerWalking : MonoBehaviour {
    public float StepSizeAKASpeed = 1f;
    public float totalDistance = 100f;
    private WaterGameLogic wg = WaterGameLogic.Instance;
	// Use this for initialization
	void Start () {
        wg.distance = totalDistance;
        if(WaterGameLogic.Instance == null)
        {
            Debug.Log("FUCK!");
        }
	}
	
	// Update is called once per frame
	void Update () {
        wg.Walk(StepSizeAKASpeed * wg.water * Time.deltaTime);
	}
}

using UnityEngine;
using System.Collections;
using System;

public class WaterBasket : MonoBehaviour {

    public GameObject Camera;
    public float AmountOfWater = 0f;
    public float BucketSize = 1f;

    private float currentAngel;
    private Vector3 axis = Vector3.zero;
    private WaterGameLogic wgLogic;
	// Use this for initialization
	void Start () {
        wgLogic = WaterGameLogic.Instance;
        wgLogic.maxWater = BucketSize;
        wgLogic.water = AmountOfWater;
	}
	
	// Update is called once per frame
	void Update () {
        try
        {
            if (!wgLogic.GameOver)
            {
                //Debug.Log("Water LEVEL:" + wgLogic.water);
                Camera.transform.rotation.ToAngleAxis(out currentAngel, out axis);
                //converting from Rad to Degrees
                //Debug.Log("ROT:" + currentAngel);
                var drippingAngel = 90 * (BucketSize - AmountOfWater) / BucketSize;
                if (currentAngel > 90)
                {
                    wgLogic.DropWaterPack();
                    
                }
                if (currentAngel > drippingAngel + 1)
                {
                    //TODO: Double check the logic
                    wgLogic.LoseSomeWater(((currentAngel) * BucketSize / 90) * Time.deltaTime);
                }
            }
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }

        //Tilt the basket
        transform.rotation = Camera.transform.rotation;
	}
}

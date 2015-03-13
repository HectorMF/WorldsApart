using UnityEngine;
using System.Collections;
using System;

public class WaterBasket : MonoBehaviour {

    public GameObject Camera;
    public float AmountOfWater = 100f;

    private float currentAngel;
    private Vector3 axis = Vector3.zero;
    private WaterGameLogic wgLogic;
	// Use this for initialization
	void Start () {
        wgLogic = WaterGameLogic.Instance;
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
                float drippingAngel = 90 * ((WaterGameResources.Instance.BucketSizeValue - wgLogic.water) / WaterGameResources.Instance.BucketSizeValue);

                //Debug.Log("DrippingAngel" + drippingAngel);
                if (currentAngel > 90)
                {
                    wgLogic.DropWaterPack();
                    
                }
                if (currentAngel > drippingAngel)
                {
                    
                    //TODO: Double check the logic
                    wgLogic.LoseSomeWater(((currentAngel) * (WaterGameResources.Instance.BucketSizeValue) / 90f * Time.fixedDeltaTime));
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

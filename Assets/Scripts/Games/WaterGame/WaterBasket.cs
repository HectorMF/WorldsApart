using UnityEngine;
using System.Collections;
using System;

public class WaterBasket : MonoBehaviour {

    public GameObject Camera;
    public float AmountOfWater = 100f;
    private float currentAngel;
    private Vector3 axis = Vector3.zero;
    private WaterGameLogic wgLogic;
    private WaterBucketAnimationHandler animationHandler;
    
	// Use this for initialization
	void Start () {
        wgLogic = WaterGameLogic.Instance;
        wgLogic.water = AmountOfWater;
        try { animationHandler = gameObject.GetComponent<WaterBucketAnimationHandler>(); }
        catch (Exception e) { Debug.LogException(e); }
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
                    Debug.Log(currentAngel - drippingAngel);
                    Debug.Log("CA" + currentAngel);
                    var diff = currentAngel - drippingAngel;
                    if(diff < 0.01 && diff > 0)
                    {
                        animationHandler.DripWater(this.gameObject, 1);
                    }
                    else if (diff < 0.14 && diff > 0.01 && currentAngel != 0)
                    {
                        animationHandler.DripWater(this.gameObject, 1);
                    }
                    else if (diff > 0.14 && diff < 0.2)
                    {
                    //    animationHandler.DripWater(this.gameObject, 2);
                   //     animationHandler.DripWater(this.gameObject, 4);
                    }
                    else if (diff > 0.2 && diff < 0.3 )
                    {
                    //    animationHandler.DripWater(this.gameObject, 3);
                    //    animationHandler.DripWater(this.gameObject, 4);
                    }
                    wgLogic.LoseSomeWater(((currentAngel) * (WaterGameResources.Instance.BucketSizeValue) / 90f * Time.fixedDeltaTime));
                }
                else
                {
                    animationHandler.DripWater(this.gameObject, 0);
                }
            }
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }

       
	}
}

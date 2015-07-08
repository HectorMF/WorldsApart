using UnityEngine;
using System.Collections;
using System;

public class WaterBasket : MonoBehaviour {

    public GameObject Camera;
    private float currentAngel;
    private Vector3 axis = Vector3.zero;
    private WaterBucketAnimationHandler animationHandler;
    
	// Use this for initialization
	void Start () {
        try { animationHandler = gameObject.GetComponent<WaterBucketAnimationHandler>(); }
        catch (Exception e) { Debug.LogException(e); }
	}
	
	// Logic is located in late update so it can change stuff after the update
	void Update () {
		if (WaterGameLogic.Instance != null && WaterGameLogic.Instance.ReadyToPlay == false) return;
        try
        {
            if (!WaterGameLogic.Instance.GameOver)
            {
                Camera.transform.rotation.ToAngleAxis(out currentAngel, out axis);
                //Calculating the angel that player starts losing water
                float drippingAngel = 90 * ((WaterGameLogic.Instance.BucketSizeValue - WaterGameLogic.Instance.water) / WaterGameLogic.Instance.BucketSizeValue);
                //See if the angel is more than 90 in which case player loses the game
                if (currentAngel > 90)
                {
                    WaterGameLogic.Instance.DropWaterPack();
                    
                }
                //Losing water
                if (currentAngel > drippingAngel)
                {

                    var diff = currentAngel - drippingAngel;
                    if(diff < 0.01 && diff > 0)
                    {
                        animationHandler.DripWater(this.gameObject, 1);
                    }
                    else if (diff > 0.01 && currentAngel != 0)
                    {
                        animationHandler.DripWater(this.gameObject, 2);
                    }
                    WaterGameLogic.Instance.LoseSomeWater(((currentAngel) * (WaterGameLogic.Instance.BucketSizeValue) / 90f * Time.fixedDeltaTime));
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

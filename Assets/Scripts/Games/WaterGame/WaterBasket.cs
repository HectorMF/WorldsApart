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
                    Debug.Log(currentAngel - drippingAngel);

                    if (currentAngel - drippingAngel < 0.14 && currentAngel - drippingAngel > 0.09)
                    {
                        AnimationHandler.DripWater(this.gameObject, 1);
                    }
                    else if (currentAngel - drippingAngel > 0.2)
                    {
                             AnimationHandler.DripWater(this.gameObject, 1);
                    }
                    wgLogic.LoseSomeWater(((currentAngel) * (WaterGameResources.Instance.BucketSizeValue) / 90f * Time.fixedDeltaTime));
                }
            }
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        float y;
       // Debug.Log(Camera.transform.rotation.eulerAngles.z);
        if(Camera.transform.rotation.eulerAngles.z > 180 && Camera.transform.rotation.eulerAngles.z < 360)
        {
            y = 180;
        }
        else
        {
            y = 0;
        }
        transform.rotation = Quaternion.Euler(new Vector3(Camera.transform.rotation.eulerAngles.x, y, Camera.transform.rotation.eulerAngles.z));
       
	}
     public class AnimationHandler
    {
         public static void DripWater(GameObject gameObject, int strength)
         {
             
             try
             {
                 Animator animator = gameObject.GetComponent<Animator>();
                 
                 animator.enabled = true;
                    
                 
                 animator.speed = 0;
                 if (strength<5)
                 {
                     //animator.
                     animator.Play("0");
                     animator.Play("1");
                     animator.speed = 1;
                   //  animator.enabled = false;
                     
                 }
                 else
                 {
                     animator.Play("2");
                     animator.Play("3");
                     animator.speed = 1;
                 }
                 
              //   animator.Play(1);
               //  animator.Play("BucketWaterGameBIG_2");
                // animator.Play("BucketWaterGameBIG_3");
                 
             }
             catch(Exception e)
             {
                 Debug.LogException(e);
             }
         }
    }
}

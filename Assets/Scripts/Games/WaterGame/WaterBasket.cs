﻿using UnityEngine;
using System.Collections;
using System;

public class WaterBasket : MonoBehaviour {

    public GameObject Camera;
    public float AmountOfWater = 0f;
    public float BasketSize = 1f;

    private float currentAngel;
    private Vector3 axis = Vector3.zero;
    private WaterGameLogic wgLogic;
	// Use this for initialization
	void Start () {
        wgLogic = WaterGameLogic.Instance;
        wgLogic.maxWater = BasketSize;
        wgLogic.water = AmountOfWater;
	}
	
	// Update is called once per frame
	void Update () {
        //TODO: Test to make sure this is right way to do this
        try
        {
            Debug.Log("Water LEVEL:" + wgLogic.water);
            Camera.transform.rotation.ToAngleAxis(out currentAngel, out axis);
            //converting from Rad to Degrees
            currentAngel = Mathf.Rad2Deg * currentAngel;
            var drippingAngel = 270 * (BasketSize - AmountOfWater) / BasketSize;
            if (currentAngel > 180)
            {
                wgLogic.DropWaterPack();
            }
            if (currentAngel > drippingAngel)
            {
                //TODO: Double check the logic
                wgLogic.LoseSomeWater(((currentAngel - drippingAngel) * BasketSize / 270) * Time.deltaTime);
            }
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
	}
}

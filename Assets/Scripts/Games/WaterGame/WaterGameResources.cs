﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaterGameResources: MonoBehaviour {
    
    private static WaterGameResources instance;
    public float TravelDistanceValue = 0f;
    public float BucketSizeValue = 100f;
    public float TrippingChance = 40f;
    public Text DistanceText;
    public GameObject Fader;
    private WaterGameResources()
    {

    }
    public static WaterGameResources Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<WaterGameResources>();
                if (instance == null)
                {
                    GameObject go = new GameObject();
                    DontDestroyOnLoad(go);
                    instance = go.AddComponent<WaterGameResources>();
                }
            }
            return instance;
        }
    }

    public Vector3 Acceleration
    {
        get
        {
            var camera = GameObject.FindObjectOfType<CameraTilt>();
            return camera.acceleration;
        }
    }
}

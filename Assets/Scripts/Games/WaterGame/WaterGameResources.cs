using UnityEngine;
using System.Collections;

public class WaterGameResources {
    private static WaterGameResources instance;
    private WaterGameResources()
    {

    }
    public static WaterGameResources Instance
    {
        get
        {
            if(instance==null)
            {
                instance = new WaterGameResources();
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

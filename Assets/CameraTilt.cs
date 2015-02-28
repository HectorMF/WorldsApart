using UnityEngine;
using System.Collections;

public class CameraTilt : MonoBehaviour {
    float accUpdateInterval = 1 / 60f;
    float lowPassKernelInS = 1f;
    float LowPassFilterFactor;
    Vector3 lowPassValue;
    void Start()
    {
        LowPassFilterFactor = accUpdateInterval / lowPassKernelInS;
        lowPassValue = Input.acceleration;
    }
    Vector3 LowPassFilterAccelerometer()
    {
        lowPassValue.x = Mathf.Lerp(lowPassValue.x, Input.acceleration.x, LowPassFilterFactor);

        return lowPassValue;
    }
	// Update is called once per frame
	void Update () {

        
        var dir = new Vector3();
        dir.x = Input.acceleration.x;
        if(dir.sqrMagnitude > 1)
        {
            dir.Normalize();
        }
        

       
	}
    float Cap(float input)
    {
 
        if(input > 180)
        {
            return 180f;
        }
        else if(input < -180f)
        {
            return -180f;
        }
        else
        {
            return input;
        }
    }
}

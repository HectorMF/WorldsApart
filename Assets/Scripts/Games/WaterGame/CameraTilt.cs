using UnityEngine;
using System.Collections;
using System;

public class CameraTilt : MonoBehaviour {
    public const float AccelSmoothing = 0.8f;
    public const float AccelLowPass = 0.01f;
    public Vector3 acceleration;
    public bool DebugMode = false;
    public float TrippingChance = 40f;

    private Vector3 _previousAcceleration;
    private Vector3 _smoothAcceleration;
    private bool isCameraFlipped = false;
    void Start()
    {
        _previousAcceleration = Vector3.zero;
       //As soon as it starts, it disables the CameraFit so it doesn't flip the screen
        Screen.autorotateToLandscapeLeft = false;
        Screen.autorotateToLandscapeRight = false;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
    }
	// Update is called once per frame
	void Update () {
        float acc;
        if(Input.GetKeyDown(KeyCode.D))
            DebugMode = true;

        if(DebugMode)
        {
            acc = 0;
            if(Input.GetKey(KeyCode.LeftArrow))
            {
                acc -= 0.1f;
            }
            if(Input.GetKey(KeyCode.RightArrow))
            {
                acc += 0.1f;
            }
        }
        else
        {

        acc = (float)Decimal.Round((decimal)Input.acceleration.x,1);

        }
        acc = acc * AccelSmoothing;
        //Debug.Log("acc" + acc);

        _smoothAcceleration = Vector3.Lerp(acceleration, _previousAcceleration, AccelSmoothing);
        _previousAcceleration = acceleration;

        
        GetComponent<Rigidbody2D>().AddTorque(acc);
        
        
        //Debug.Log("Camera:" + acceleration.x);
        float a = 0f;
        Vector3 b = Vector3.zero;
        transform.rotation.ToAngleAxis(out a, out b);
        a = a * Mathf.Rad2Deg;
  
	}
    void LateUpdate()
    {
        //Debug.Log("Rotation" + a);
        //Making sure the WaterGameLogic is not between another Camera Shake move
        if (WaterGameLogic.Instance.Step == 0)
        {
            var r = UnityEngine.Random.Range(0, 100);
            if (r < TrippingChance)
            {
                var angel = UnityEngine.Random.Range(-5, 5);
                //Debug.Log("What?" + angel);
                WaterGameLogic.Instance.CameraRandomMove(angel, this.gameObject);
            }
        }
        else
        {
            WaterGameLogic.Instance.CameraRandomMove(0, this.gameObject);
        }
    }
}


using UnityEngine;
using System.Collections;
using System;

public class CameraTilt : MonoBehaviour {
    public const float AccelSmoothing = 0.8f;
    public const float AccelLowPass = 0.01f;
    public Vector3 acceleration;

    private Vector3 _previousAcceleration;
    private Vector3 _smoothAcceleration;
    void Start()
    {
        _previousAcceleration = Vector3.zero;
    }
	// Update is called once per frame
	void Update () {
        //Some Hackish converting going on here!
        float acc = (float)Decimal.Round((decimal)Input.acceleration.x,1);
        acc = acc * AccelSmoothing;
        //Debug.Log("acc" + acc);

        acceleration = Input.acceleration;
        acceleration.x = (acceleration.x * AccelSmoothing) + (_previousAcceleration.x * (1f - AccelLowPass));
        acceleration.y = (acceleration.y * AccelSmoothing) + (_previousAcceleration.y * (1f - AccelLowPass));

        _smoothAcceleration = Vector3.Lerp(acceleration, _previousAcceleration, AccelSmoothing);
        _previousAcceleration = acceleration;

        //transform.rotation = Quaternion.Euler(0,0,acceleration.x);
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
            if (r < WaterGameResources.Instance.TrippingChance)
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


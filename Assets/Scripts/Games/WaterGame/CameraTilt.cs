using UnityEngine;
using System.Collections;

public class CameraTilt : MonoBehaviour {
    public const float AccelSmoothing = 0.9f;
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
        acceleration = Input.acceleration;
        acceleration.x = (acceleration.x * AccelSmoothing) + (_previousAcceleration.x * (1f - AccelLowPass));
        acceleration.y = (acceleration.y * AccelSmoothing) + (_previousAcceleration.y * (1f - AccelLowPass));

        _smoothAcceleration = Vector3.Lerp(acceleration, _previousAcceleration, AccelSmoothing);
        _previousAcceleration = acceleration;

        transform.rotation = Quaternion.Euler(0,0,acceleration.x);
        //Debug.Log("Camera:" + acceleration.x);
        float a = 0f;
        Vector3 b = Vector3.zero;
        transform.rotation.ToAngleAxis(out a, out b);
        a = a * Mathf.Rad2Deg;
        //Debug.Log("Rotation" + a);


       
	}
}


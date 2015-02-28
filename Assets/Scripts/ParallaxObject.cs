using UnityEngine;
using System.Collections;
/*
 * TEMPORARY FOR DELETION
 */
public class ParallaxObject : MonoBehaviour {
    public static float xOffset = 0;
    public static float yOffset = 0;

    public float xScrollSpeed;
    public float yScrollSpeed;
    
      private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

	void Update () {
        transform.position = new Vector3(startPosition.x + xOffset * xScrollSpeed, startPosition.y + yOffset * yScrollSpeed, startPosition.z);
	}
}
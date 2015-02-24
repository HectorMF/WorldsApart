using UnityEngine;
using System.Collections;

public class ParallaxLayer : MonoBehaviour {
    public float xScrollSpeed;
    public float yScrollSpeed;

    private Vector3 startPosition;

    void Start()
    {
        ParallaxController.Instance.AddLayer(this);
        startPosition = transform.position;
    }

	public void UpdateOffsets (float xOffset, float yOffset) {
        transform.position = new Vector3(startPosition.x + xOffset * xScrollSpeed, startPosition.y + yOffset * yScrollSpeed, startPosition.z);
	}
}

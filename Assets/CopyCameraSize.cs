using UnityEngine;
using System.Collections;

public class CopyCameraSize : MonoBehaviour {
	public Camera otherCamera;
	private Camera thisCamera;
	// Use this for initialization
	void Start () {
		thisCamera = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		thisCamera.orthographicSize = otherCamera.orthographicSize;
		thisCamera.rect = otherCamera.rect;
	}
}

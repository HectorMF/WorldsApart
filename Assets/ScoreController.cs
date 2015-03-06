using UnityEngine;
using System.Collections;

public class ScoreController : MonoBehaviour {

	public float Food, Water;

	// Use this for initialization
	void Start () {
		Object.DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.S)) Debug.Log (Food);
	}

}

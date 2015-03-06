using UnityEngine;
using System.Collections;

public class ScoreController : MonoBehaviour {

	public float Food, Water;

	// Use this for initialization
	void Start () {
		Food = 0;
		Water = 0;
		Object.DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.S)) Debug.Log (Food);
	}

	void OnLevelWasLoaded(int level) {
		if (level == 1)
		{
			ThirdWorldManager.Instance.IncrementFood((int)Food);
			Food = 0;
		}
	}
}

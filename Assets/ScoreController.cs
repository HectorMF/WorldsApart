using UnityEngine;
using System.Collections;

public class ScoreController : MonoBehaviour {

	public float Food, Water, Mood;

	// Use this for initialization
	void Start () {
		Food = 0;
		Water = 0;
        Mood = 0;
		Object.DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.S)) Debug.Log ("Food: " + Food + ", Water: " + Water + ", Mood: " + Mood);
	}

	void OnLevelWasLoaded(int level) {
		if (level == 1)
		{
			ThirdWorldManager.Instance.IncrementFood((int)Food);
            ThirdWorldManager.Instance.IncrementWater((int)Water);
            //TODO: Handle Mood
			Food = 0;
		}
	}
}

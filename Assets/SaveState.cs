using UnityEngine;
using System.Collections;

public class SaveState : MonoBehaviour {

	Vector3 position;
	Quaternion rotation;

	void Awake () {
		GameObject.DontDestroyOnLoad (gameObject);
	//	SetChildrenActive(false);
	}

	void OnLevelWasLoaded(int level)
	{
//		Debug.Log(Application.loadedLevelName);
		if (Application.loadedLevelName == "WorldsApart") {
			SetChildrenActive(true);
		} else {
			SetChildrenActive(false);
		}
	}

	void SetChildrenActive(bool b)
	{
		foreach (Transform child in transform) {
			child.gameObject.SetActive (b);
			if (child.name == "Weather" && !b && Application.loadedLevelName != "Dinner Minigame")
				child.gameObject.SetActive(true);
		}
	}
}

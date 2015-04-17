using UnityEngine;
using System.Collections;

public class SaveState : MonoBehaviour {

	Vector3 position;
	Quaternion rotation;

	void Awake () {
		GameObject.DontDestroyOnLoad (gameObject);
	}

	void OnLevelWasLoaded(int level)
	{
		if (level != 1) {
			SetChildrenActive(false);
		} else {
			SetChildrenActive(true);
		}
	}

	void SetChildrenActive(bool b)
	{
		foreach (Transform child in transform)
			child.gameObject.SetActive (b);
	}
}

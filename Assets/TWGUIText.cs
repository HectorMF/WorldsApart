using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TWGUIText : MonoBehaviour {
	private Text text;
	public enum Resource { Food = 0, Water = 1, Mood = 2};
	public Resource gameResource;
	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		if(gameResource == Resource.Food)
			text.text = ThirdWorldManager.Instance.CurrentFood + "";
		if(gameResource == Resource.Water)
			text.text = ThirdWorldManager.Instance.CurrentWater + "";
		if(gameResource == Resource.Mood)
			text.text = ThirdWorldManager.Instance.CurrentMood + "";
	}
}

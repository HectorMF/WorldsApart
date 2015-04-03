using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpdateFood : MonoBehaviour {
	public Text text;
	public ScoreController controller;
	// Use this for initialization
	void Start () {
		controller = GameObject.Find("ScoreController").GetComponent<ScoreController>();
	}
	
	// Update is called once per frame
	void Update () {
		text.text = string.Format("{0:0.00}",controller.Food) + " Food";
	}
}

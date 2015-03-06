using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class Fade : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
        this.GetComponent<Image>().DOFade(0, 2);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

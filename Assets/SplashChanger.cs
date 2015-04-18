using UnityEngine;
using System.Collections;
using DG.Tweening;

public class SplashChanger : MonoBehaviour {
	public CanvasGroup group1;
	public CanvasGroup group2;
	public float duration = 3;
	// Use this for initialization
	void Start () {
		group2.alpha = 0;
		group1.alpha = 0;
		group1.DOFade(1,duration).OnComplete(()=>{group1.DOFade(0,1);group2.DOFade(1, duration).OnComplete(()=>Fader.FadeOutIn(Fader.Gesture.None,"","",()=>Application.LoadLevel("Menu")));});
	}
}

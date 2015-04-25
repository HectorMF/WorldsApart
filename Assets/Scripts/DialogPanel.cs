using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using WorldsApart.Clickables;

public class DialogPanel : MonoBehaviour {
	public AudioClip clip;
	public CanvasGroup fade;
	private Text textBox;
	
	public TweenCallback openAction;
	public TweenCallback closeAction;
	
	void Start () {
		textBox = GameObject.Find("textBox").GetComponentInChildren<Text>();
	}
	
	public void Open(string text){
		//fade.DOFade(1,.5f);
		//Clickable.enabledAll = false;
		AudioSource.PlayClipAtPoint(clip, transform.position);
		transform.localScale = Vector3.zero;	
		transform.DOScale(Vector3.one,.5f).SetEase(Ease.OutExpo).OnComplete(openAction);

		textBox.text = text;
	}
	
	public void Close(){
		//Clickable.enabledAll = true;
		//fade.DOFade(0,.5f);
		transform.DOScale(Vector3.zero,.5f).SetEase(Ease.OutExpo).OnComplete(closeAction);
	}
}

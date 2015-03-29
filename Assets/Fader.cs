using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class Fader : MonoBehaviour {
	public enum Gesture { None = 0, Tap = 1, Swipe = 2, Balance = 3 }
	private static Fader instance;

	public Text title;
	public Text subTitle;
	public CanvasGroup group;

	public GameObject swipe;
	public GameObject tap;
	public GameObject balance;

	void Awake () {
		instance = this;
	}

	private void SetGesture(Gesture gesture)
	{
		swipe.SetActive(false);
		tap.SetActive(false);
		balance.SetActive(false);

		if(gesture == Gesture.Swipe)
			swipe.SetActive(true);
		if(gesture == Gesture.Tap)
			tap.SetActive(true);
		if(gesture == Gesture.Balance)
			balance.SetActive(true);
	}

	public static void FadeToBlack(float delay = 0, float duration = 1, string title = "", string subtitle = "", DG.Tweening.TweenCallback action = null)
	{
		FadeToBlack(Gesture.None, delay, duration, title, subtitle, action);
	}

	public static void FadeToClear(float delay = 0, float duration = 1, string title = "", string subtitle = "", DG.Tweening.TweenCallback action = null)
	{
		FadeToClear(Gesture.None, delay, duration, title, subtitle, action);
	}

	public static void FadeToBlack(Gesture gesture, float delay = 0, float duration = 1, string title = "", string subtitle = "", DG.Tweening.TweenCallback action = null)
	{
		instance.SetGesture(gesture);
		instance.title.text = title;
		instance.subTitle.text = subtitle;
		instance.group.DOFade(1, duration).SetDelay(delay).OnComplete(action);
	}
	
	public static void FadeToClear(Gesture gesture, float delay = 0, float duration = 1, string title = "", string subtitle = "", DG.Tweening.TweenCallback action = null)
	{
		instance.SetGesture(gesture);
		instance.title.text = title;
		instance.subTitle.text = subtitle;
		instance.group.DOFade(0, duration).SetDelay(delay).OnComplete(action);
	}
}

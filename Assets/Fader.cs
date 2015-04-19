using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class Fader : MonoBehaviour {
	public enum Gesture { None = 0, Tap = 1, Swipe = 2, Balance = 3 }
	private static Fader instance;

	public float duration = 1;
	public float delay = 3;

	public Text title;
	public Text subTitle;
	public CanvasGroup group;

	public GameObject swipe;
	public GameObject tap;
	public GameObject balance;

	static private Fader Instance
	{
		get
		{
			if (instance == null)
			{
				instance = Object.FindObjectOfType(typeof(Fader)) as Fader;
				
				if (instance == null)
				{
					GameObject go = (GameObject)Instantiate(Resources.Load("Fader Canvas"));
					DontDestroyOnLoad(go);
					instance = go.GetComponentsInChildren<Fader>()[0];
				}
			}
			return instance;
		}
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

	public static void FadeToBlack(Gesture gesture, float delay = 0, float duration = 1, string title = "", string subtitle = "", DG.Tweening.TweenCallback action = null,int titleFont = 50, int subTitleFont = 30)
	{
		Instance.group.alpha = 0;
		Instance.SetGesture(gesture);
		Instance.title.text = title;
		Instance.subTitle.text = subtitle;
		Instance.title.fontSize = titleFont;
		Instance.subTitle.fontSize = subTitleFont;
		Instance.group.DOFade(1, duration).SetDelay(delay).SetEase(Ease.InSine).OnComplete(action);
	}
	
	public static void FadeToClear(Gesture gesture, float delay = 0, float duration = 1, string title = "", string subtitle = "", DG.Tweening.TweenCallback action = null, int titleFont = 50, int subTitleFont = 30)
	{
		Instance.group.alpha = 1;
		Instance.SetGesture(gesture);
		Instance.title.text = title;
		Instance.subTitle.text = subtitle;
		Instance.title.fontSize = titleFont;
		Instance.subTitle.fontSize = subTitleFont;
		Instance.group.DOFade(0, duration).SetDelay(delay).SetEase(Ease.OutSine).OnComplete(action);
	}

	public static void FadeOutIn(Gesture gesture, string title = "", string subtitle = "", DG.Tweening.TweenCallback action = null, int titleFont = 50, int subTitleFont = 30, DG.Tweening.TweenCallback actionOnEnd = null)
	{
		FadeToBlack(gesture, 0, Fader.Instance.duration, title, subtitle, ()=>
		            {FadeToClear(gesture, Fader.Instance.delay, Fader.instance.duration, title, subtitle, actionOnEnd, titleFont, subTitleFont); action();}, titleFont, subTitleFont);
	}
}

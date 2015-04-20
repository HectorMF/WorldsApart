using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class Fader : MonoBehaviour {
	public enum Gesture { None = 0, Tap = 1, Swipe = 2, Balance = 3 }
	private static Fader instance;

	public Text titleText;
	public Text subTitleText;
	public CanvasGroup group;

	public GameObject swipe;
	public GameObject tap;
	public GameObject balance;

	private Gesture gesture;
	private float duration;
	private float delay;
	private string title;
	private string subtitle;
	private DG.Tweening.TweenCallback fadeOutAction;
	private DG.Tweening.TweenCallback fadeInAction;
	private int titleSize;
	private int subTitleSize;
	private Ease ease;

	static public Fader Instance
	{
		get
		{
			if (instance == null)
			{
				instance = GameObject.FindObjectOfType(typeof(Fader)) as Fader;
				
				if (instance == null)
				{
					GameObject go = (GameObject)Instantiate(Resources.Load("Fader Canvas"));
					DontDestroyOnLoad(go);
					instance = go.GetComponentsInChildren<Fader>()[0];
					instance.ResetFader();
				}
			}
			return instance;
		}
	}

	private void ConfigureFader()
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

		titleText.text = title;
		subTitleText.text = subtitle;
		titleText.fontSize = titleSize;
		subTitleText.fontSize = subTitleSize;
	}
	
	private void ResetFader()
	{
		gesture = Gesture.None;
		subtitle = "";
		title = "";
		titleSize = 50;
		subTitleSize = 30;
		delay = 0;
		ease = Ease.InSine;
		fadeOutAction = null;
		fadeInAction = null;
		duration = 2;
	}

	public Fader SetGesture(Gesture gesture)
	{
		this.gesture = gesture;
		return this;
	}

	public Fader SetTitle(string title)
	{
		this.title = title;
		return this;
	}

	public Fader SetSubTitle(string subTitle)
	{
		this.subtitle = subTitle;
		return this;
	}

	public Fader SetDelay(float delay)
	{
		this.delay = delay;
		return this;
	}

	public Fader SetFadeDuration(float duration)
	{
		this.duration = duration;
		return this;
	}

	public Fader FadeOutOnComplete(DG.Tweening.TweenCallback action)
	{
		fadeOutAction = action;
		return this;
	}

	public Fader FadeInOnComplete(DG.Tweening.TweenCallback action)
	{
		fadeInAction = action;
		return this;
	}

	public Fader SetTitleSize(int size)
	{
		titleSize = size;
		return this;
	}

	public Fader SetSubTitleSize(int size)
	{
		subTitleSize = size;
		return this;
	}

	public Fader SetEase(Ease ease)
	{
		this.ease = ease;
		return this;
	}

	public void FadeOut()
	{
		group.alpha = 0;
		ConfigureFader();
		group.DOFade(1, duration).SetDelay(delay).SetEase(ease).OnComplete(fadeOutAction);
		ResetFader();
	}

	public void FadeIn()
	{
		group.alpha = 1;
		ConfigureFader();
		group.DOFade(0, duration).SetDelay(delay).SetEase(ease).OnComplete(fadeInAction);
		ResetFader();
	}

	public void FadeOutIn()
	{
		group.alpha = 0;
		ConfigureFader();
		group.DOFade(1, duration)
			.SetDelay(delay)
			.SetEase(ease)
			.OnComplete(
				()=> 
				{ 
					if(fadeOutAction != null)
						fadeOutAction();
					group.DOFade(0, duration).SetDelay(delay).SetEase(ease).OnComplete(
						()=>{
							if(fadeInAction != null)
								fadeInAction(); 
							ResetFader();
						});
				});
	}
}

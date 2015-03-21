using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class Fader : MonoBehaviour {
	private static Fader instance;

	public Text title;
	public Text subTitle;
	public CanvasGroup group;

	void Awake () {
		instance = this;
	}

	public static void FadeToBlack(float delay = 0, float duration = 1, string title = "", string subtitle = "", DG.Tweening.TweenCallback action = null)
	{
		instance.title.text = title;
		instance.subTitle.text = subtitle;
		instance.group.DOFade(1, duration).SetDelay(delay).OnComplete(action);
	}

	public static void FadeToClear(float delay = 0, float duration = 1, string title = "", string subtitle = "", DG.Tweening.TweenCallback action = null)
	{
		instance.title.text = title;
		instance.subTitle.text = subtitle;
		instance.group.DOFade(0, duration).SetDelay(delay).OnComplete(action);
	}
}

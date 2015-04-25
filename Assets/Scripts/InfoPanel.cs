using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using WorldsApart.Clickables;

public class InfoPanel : MonoBehaviour {
	public AudioClip clip;
	public CanvasGroup fade;
	private Text requiredFood;
	private Text requiredWater;
	private Text requiredMood;

	private Text rewardsFood;
	private Text rewardsWater;
	private Text rewardsMood;

	public TweenCallback openAction;
	public TweenCallback closeAction;
	public TweenCallback acceptAction;
	
	void Start () {
		requiredFood = GameObject.Find("reqFood").GetComponentInChildren<Text>();
		requiredWater = GameObject.Find("reqWater").GetComponentInChildren<Text>();
		requiredMood = GameObject.Find("reqMood").GetComponentInChildren<Text>();

		rewardsFood = GameObject.Find("rewFood").GetComponentInChildren<Text>();
		rewardsWater = GameObject.Find("rewWater").GetComponentInChildren<Text>();
		rewardsMood = GameObject.Find("rewMood").GetComponentInChildren<Text>();
	}

	public void Open(int reqFood, int reqWater, int reqMood,
	                 int rewFood, int rewWater, int rewMood){
		fade.DOFade(1,.5f);
		Clickable.enabledAll = false;
		AudioSource.PlayClipAtPoint(clip, transform.position);
		transform.localScale = Vector3.zero;	
		transform.DOScale(Vector3.one,.5f).SetEase(Ease.OutExpo).OnComplete(openAction);

		requiredFood.text = reqFood + "";
		requiredWater.text = reqWater + "";
		requiredMood.text = reqMood + "";

		rewardsFood.text = Reward(rewFood);
		rewardsWater.text = Reward(rewWater);
		rewardsMood.text = Reward(rewMood);
	}

	private string Reward(int amount)
	{
		if (amount > 0)
			return "+";
		else if (amount < 0)
			return "-";
		else
			return "0";
	}

	public void Close(){
		Clickable.enabledAll = true;
		fade.DOFade(0,.5f);
		transform.localScale = Vector3.one;
		transform.DOScale(Vector3.zero,.5f).SetEase(Ease.OutExpo).OnComplete(closeAction);
	}

	public void Accept()
	{
		Clickable.enabledAll = true;
		fade.DOFade(0,.5f);
		transform.localScale = Vector3.one;
		transform.DOScale(Vector3.zero,.5f).SetEase(Ease.OutExpo).OnComplete(acceptAction);
	}
}

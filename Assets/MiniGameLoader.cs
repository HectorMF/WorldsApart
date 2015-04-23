using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using DG.Tweening;
using WorldsApart.GUI;

public class MiniGameLoader : MonoBehaviour {
	//Dialog When Requirements not met
	public string dialogText1;
	public string dialogText2;
	//Scene to load and fader info
	public int gamesPerDay = 1;
	public string sceneName;
	public string title, subTitle;
	public Fader.Gesture gesture = Fader.Gesture.None;

	//requirements and rewards, can be changed.
	public int RequiredWater, RequiredFood, RequiredMood;
	public int RewardWater, RewardFood, RewardMood;

	private InfoPanel panel;
	private DialogPanel dialog;
	private GameObject actionIndicator;
	private Thirst thirst;
	private int timesPlayed = 0;

	public void Start(){
		actionIndicator = transform.FindChild("Action").gameObject;
		panel = GameObject.Find("InfoPanel").GetComponent<InfoPanel>();
		dialog = GameObject.Find("DialogPanel").GetComponent<DialogPanel>();
	}

	public void Load()
	{
		dialog.Close();
		thirst = transform.parent.GetComponent<Thirst> ();
		if (thirst != null) RequiredWater = thirst.CurrentWaterRequirement;
		
		if (RequirementsMet ()) {
			panel.acceptAction = LoadMiniGame;
			panel.Open(RequiredFood, RequiredWater, RequiredMood, RewardFood, RewardWater, RewardMood);
		}else{
			if(timesPlayed >= gamesPerDay) 
				dialog.Open(dialogText2);
			else
				dialog.Open(dialogText1);
		}
	}

	private void LoadMiniGame()
	{
		timesPlayed ++;
		if(thirst)
			thirst.Drink();
		actionIndicator.transform.DOKill();
		actionIndicator.GetComponent<Wobble>().enabled = false;
		actionIndicator.transform.DOScale(Vector3.zero, 1f);

		Fader.Instance
			.SetGesture(gesture)
			.SetTitle(title)
			.SetSubTitle(subTitle)
			.FadeOutOnComplete(()=>Application.LoadLevel(sceneName))
			.FadeOutIn();
	}

	public void Update()
	{
		if(RequirementsMet())
		{
			actionIndicator.transform.DOKill();
			actionIndicator.GetComponent<Wobble>().enabled = true;
			actionIndicator.transform.DOScale(Vector3.one, 1f);
		}else{
			actionIndicator.transform.DOKill();
			actionIndicator.GetComponent<Wobble>().enabled = false;
			actionIndicator.transform.DOScale(Vector3.zero, 1f);
		}
	}
	
	bool RequirementsMet()
	{
		if(gamesPerDay != -1 && timesPlayed >= gamesPerDay) return false;
		int currentFood = ThirdWorldManager.Instance.CurrentFood;
		int currentWater = ThirdWorldManager.Instance.CurrentWater;
		if (RequiredFood != 0 && RequiredWater != 0)
			return (currentFood >= RequiredFood && currentWater >= RequiredWater);
		else if (RequiredFood == 0)
			return currentWater >= RequiredWater;
		else if (RequiredWater == 0)
			return currentFood >= RequiredFood;
		else
			return false;
	}

	void OnEnable()
	{
		ThirdWorldManager.OnDayEnd += DayEnd;
	}
	
	void OnDisable()
	{
		ThirdWorldManager.OnDayEnd -= DayEnd;
	}
	
	public virtual void DayEnd()
	{
		timesPlayed = 0;
	}
}

using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using DG.Tweening;
using WorldsApart.GUI;

public class MiniGameLoader : MonoBehaviour {
	//Scene to load and fader info
	public int gamesPerDay = 1;
	public string sceneName;
	public string title, subTitle;
	public Fader.Gesture gesture = Fader.Gesture.None;

	//requirements and rewards, can be changed.
	public int RequiredWater, RequiredFood, RequiredMood;
	public int RewardWater, RewardFood, RewardMood;

	private InfoPanel panel;
	private GameObject actionIndicator;
	private Thirst thirst;
	private int timesPlayed = 0;

	public void Start(){
		actionIndicator = transform.FindChild("Action").gameObject;
		if(!RequirementsMet()){
			GetComponent<BoxCollider> ().enabled = false;
			actionIndicator.transform.localScale = Vector3.zero;
		}
	}

	public void Load()
	{
		panel = GameObject.Find("InfoPanel").GetComponent<InfoPanel>();

		thirst = transform.parent.GetComponent<Thirst> ();
		if (thirst != null) RequiredWater = thirst.CurrentWaterRequirement;
		
		if (RequirementsMet ()) {
			panel.acceptAction = LoadMiniGame;
			panel.Open(RequiredFood, RequiredWater, RequiredMood, RewardFood, RewardWater, RewardMood);
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
		GetComponent<BoxCollider> ().enabled = false;
		Fader.FadeOutIn(gesture, title, subTitle, ()=>Application.LoadLevel(sceneName));
	}

	public void Update()
	{
		if(RequirementsMet())
		{
			actionIndicator.transform.DOKill();
			actionIndicator.GetComponent<Wobble>().enabled = true;
			actionIndicator.transform.DOScale(Vector3.one, 1f);
			GetComponent<BoxCollider> ().enabled = true;
		}else{
			actionIndicator.transform.DOKill();
			actionIndicator.GetComponent<Wobble>().enabled = false;
			actionIndicator.transform.DOScale(Vector3.zero, 1f);
			GetComponent<BoxCollider> ().enabled = false;
		}
	}
	
	bool RequirementsMet()
	{
		if(timesPlayed >= gamesPerDay) return false;
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

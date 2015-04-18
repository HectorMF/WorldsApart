using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using DG.Tweening;

public class MiniGameLoader : MonoBehaviour {

	//Scene to load and fader info
	public string sceneName;
	public string title, subTitle;
	public Fader.Gesture gesture = Fader.Gesture.None;

	//requirements and rewards, can be changed.
	public int RequiredWater, RequiredFood, RequiredMood;
	public int RewardWater, RewardFood, RewardMood;

	private InfoPanel panel;
	private GameObject actionIndicator;

	public void Load()
	{
		panel = GameObject.Find("InfoPanel").GetComponent<InfoPanel>();
		actionIndicator = transform.FindChild("Action").gameObject;
		Thirst thirst = transform.parent.GetComponent<Thirst> ();
		if (thirst != null) RequiredWater = thirst.CurrentWaterRequirement;
		
		//if (RequirementsMet ()) {
			panel.acceptAction = LoadMiniGame;

			panel.closeAction = ()=> actionIndicator.transform.DOScale(Vector3.one, .2f);
			panel.Open(RequiredFood, RequiredWater, RequiredMood, RewardFood, RewardWater, RewardMood);
		//}
	}

	private void LoadMiniGame()
	{
		ThirdWorldManager.Instance.UsedAction();
		GetComponent<BoxCollider> ().enabled = false;
		Fader.FadeOutIn(gesture, 0, 2, title, subTitle, ()=>Application.LoadLevel(sceneName));
	}

	public void Update()
	{
	}
	
	bool RequirementsMet()
	{
		int currentFood = ThirdWorldManager.Instance.CurrentFood;
		int currentWater = ThirdWorldManager.Instance.CurrentWater;
		if (RequiredFood != 0 && RequiredWater != 0)
			return (currentFood >= RequiredFood && currentWater >= RequiredWater) || currentFood >= RequiredFood || currentWater >= RequiredWater;
		else if (RequiredFood == 0)
			return currentWater >= RequiredWater;
		else if (RequiredWater == 0)
			return currentFood >= RequiredFood;
		else
			return false;
	}
}

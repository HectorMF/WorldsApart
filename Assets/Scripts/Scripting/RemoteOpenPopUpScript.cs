using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using WorldsApart.Clickables;

namespace WorldsApart.Scripting
{
	public class RemoteOpenPopUpScript : RemoteScript 
	{
		public Fader.Gesture gesture = Fader.Gesture.None;
		public string GameName, Title, Subtitle;
		
		public void LoadMiniGame()
		{
			ThirdWorldManager.Instance.UsedAction();
			//gameObject.transform.parent.parent.GetComponent<BoxCollider> ().enabled = false;
			//gameObject.transform.parent.gameObject.SetActive (false);
			Fader.FadeOutIn(gesture, 0, 2, Title, Subtitle, ()=>Application.LoadLevel(GameName));
		}

		public GameObject ActionIndicator;
		public int RequiredWater, RequiredFood, RequiredMood;
		public GameObject Confirm, Requirements;
		private InfoPanel panel;
		public override void Start(Action OnFinish)
		{
			panel = GameObject.Find("InfoPanel").GetComponent<InfoPanel>();
			// The pump does not have a thirst script on it
			Thirst thirst = ActionIndicator.transform.parent.parent.GetComponent<Thirst> ();
			if (thirst != null) RequiredWater = thirst.CurrentWaterRequirement;

			if (RequirementsMet ()) {
				SetConfirmActive();
			} else {
				SetConfirmInactive();
				SetRequirements();
			}
			panel.acceptAction = LoadMiniGame;
			panel.openAction = ()=> ActionIndicator.SetActive(false); 
			panel.closeAction = ()=> ActionIndicator.SetActive(true);
			panel.Open(RequiredFood, RequiredWater, RequiredMood,0,1,3);

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

		void SetConfirmActive()
		{
			Confirm.SetActive (true);
			Requirements.SetActive (false);
			Confirm.transform.parent.GetComponent<Clickable>().active = true;
		}

		void SetConfirmInactive()
		{
			Confirm.SetActive(false);
			Requirements.SetActive(true);
			Confirm.transform.parent.GetComponent<Clickable>().active = false;
		}

		void SetRequirements()
		{
			Requirements.transform.FindChild ("Food/Text").GetComponent<Text> ().text = NeededFood().ToString();
			Requirements.transform.FindChild ("Water/Text").GetComponent<Text> ().text = NeededWater().ToString();
		}

		int NeededFood()
		{
			return GetNeededResouce (RequiredFood, ThirdWorldManager.Instance.CurrentFood);
		}

		int NeededWater()
		{
			return GetNeededResouce (RequiredWater, ThirdWorldManager.Instance.CurrentWater);
		}

		int GetNeededResouce(int Required, int CurrentResource)
		{
			if (Required - CurrentResource > 0)
				return Required - CurrentResource;
			else
				return 0;
		}
	}
}

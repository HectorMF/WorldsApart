using UnityEngine;
using System.Collections;

namespace WorldsApart.Handlers
{
	public class DinnerGameHandler : Handler
	{
		public override void innerDelegate()
		{
			Debug.Log("Dinner game!");
			ThirdWorldManager.Instance.UsedAction();
			Fader.FadeToBlack(0,2,"Feed your Family!!","Touch the plates!", switchLevel);
			gameObject.SetActive(false);
		}
		
		public void switchLevel(){
			Application.LoadLevel("Dinner Minigame");
		}
	}
}
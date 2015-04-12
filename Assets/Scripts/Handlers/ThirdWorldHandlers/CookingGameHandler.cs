using UnityEngine;
using System.Collections;

namespace WorldsApart.Handlers
{
	public class CookingGameHandler : Handler {
		
		public override void innerDelegate()
		{
			Debug.Log("Cooking Game!");
			ThirdWorldManager.Instance.UsedAction();
			Fader.FadeToBlack(0,2,"Fan the Coals","Maintain a temperature between 300-350",switchLevel);
			gameObject.SetActive(false);
		}
		
		public void switchLevel(){
			Application.LoadLevel("Cooking Minigame");
		}
	}
}

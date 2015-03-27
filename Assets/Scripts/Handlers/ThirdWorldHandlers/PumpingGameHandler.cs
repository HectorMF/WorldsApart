using UnityEngine;
using System.Collections;

namespace WorldsApart.Handlers
{
	public class PumpingGameHandler : Handler {

		public override void innerDelegate()
		{
			Debug.Log("Pump Game!");
			ThirdWorldManager.Instance.UsedAction();
			Fader.FadeToBlack(0,2,"Pump Water","Time pumps for max water",switchLevel);
			gameObject.SetActive(false);
		}
		
		public void switchLevel(){
			Application.LoadLevel("Pumping Minigame");
		}
	}
}

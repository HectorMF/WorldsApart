using UnityEngine;
using System.Collections;

namespace WorldsApart.Handlers
{
	public class ShearingGameHandler : Handler {

		public override void innerDelegate()
		{
			Debug.Log("Shearing game!");
			ThirdWorldManager.Instance.UsedAction();
			Fader.FadeToBlack(0,2,"Shear the Sheep!!","Don't touch any space twice!",switchLevel);
			gameObject.SetActive(false);
		}
		
		public void switchLevel(){
			Application.LoadLevel("Shearing Minigame");
		}
	}
}
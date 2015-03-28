using UnityEngine;
using System.Collections;

namespace WorldsApart.Handlers
{
	public class MilkingGameHandler : Handler
	{
		public override void innerDelegate()
		{
			Debug.Log("Milking game!");
			ThirdWorldManager.Instance.UsedAction();
			Fader.FadeToBlack(0,2,"Milk The Cow","",switchLevel);
			gameObject.SetActive(false);
		}

		public void switchLevel()
		{
			Application.LoadLevel("Milking Minigame");
		}
	}
}
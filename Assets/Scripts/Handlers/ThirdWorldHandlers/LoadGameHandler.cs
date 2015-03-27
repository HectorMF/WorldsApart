using UnityEngine;
using System.Collections;

namespace WorldsApart.Handlers
{
	public class LoadGameHandler : Handler
	{
		public string GameName, Title, Subtitle;

		public override void innerDelegate()
		{
			ThirdWorldManager.Instance.UsedAction();
			Fader.FadeToBlack(0, 2, Title, Subtitle, switchLevel);
			gameObject.SetActive(false);
		}
		
		public void switchLevel(){
			Application.LoadLevel(GameName);
		}
	}
}
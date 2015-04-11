using UnityEngine;
using System.Collections;

namespace WorldsApart.Handlers
{
	public class LoadGameHandler : Handler
	{
		public Fader.Gesture gesture = Fader.Gesture.None;
		public string GameName, Title, Subtitle;

		public override void innerDelegate()
		{
			ThirdWorldManager.Instance.UsedAction();
			Fader.FadeOutIn(gesture, 0, 2, Title, Subtitle, ()=>Application.LoadLevel(GameName));
//			gameObject.SetActive(false);
		}
	}
}
using UnityEngine;
using System.Collections;

namespace WorldsApart.Scripting
{
	public class RemoteMilkingMinigameScript : RemoteScript
	{
		public override void Start(System.Action OnFinish)
		{
			Debug.Log("Milking game!");
			ThirdWorldManager.Instance.UsedAction();
			Fader.FadeToBlack(0,2,"Milk The Cow","",switchLevel);
			base.targetController.scripts.Clear();
			OnFinish();
		}

		public void switchLevel(){
			Application.LoadLevel("Milking Minigame");
		}
	}
}
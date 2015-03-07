using UnityEngine;
using System.Collections;

namespace WorldsApart.Scripting
{
	public class RemoteDinnerMinigameScript : RemoteScript
	{
		public override void Start(System.Action OnFinish)
		{
			Debug.Log("Dinner game!");
			ThirdWorldManager.Instance.UsedAction();
			Fader.FadeToBlack(0,2,"Feed your Family!!","Touch the plates!", switchLevel);
			base.targetController.scripts.Clear();
			OnFinish();
		}

		public void switchLevel(){
			Application.LoadLevel("Dinner Minigame");
		}
	}
}
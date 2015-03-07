using UnityEngine;
using System.Collections;

namespace WorldsApart.Scripting
{
	public class RemoteCropsMinigameScript : RemoteScript
	{
		public override void Start(System.Action OnFinish)
		{
			Debug.Log("Crops game!");
			ThirdWorldManager.Instance.UsedAction();
			Fader.FadeToBlack(0,2,"Pick Crops!!","Shoo the Animals!",switchLevel);
			base.targetController.scripts.Clear();
			OnFinish();
		}

		public void switchLevel(){
			Application.LoadLevel("Farming Minigame");
		}
	}
}
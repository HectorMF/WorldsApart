using UnityEngine;
using System.Collections;

namespace WorldsApart.Scripting
{
	public class RemotePumpingMinigameScript : RemoteScript
	{
		public override void Start(System.Action OnFinish)
		{
			Debug.Log("Pump Game!");
			ThirdWorldManager.Instance.UsedAction();
			Fader.FadeToBlack(0,2,"Pump Water","Time pumps for max water",switchLevel);
			base.targetController.scripts.Clear();
			OnFinish();
		}
		
		public void switchLevel(){
			Application.LoadLevel("Pumping Minigame");
		}
	}
}

using UnityEngine;
using System.Collections;

namespace WorldsApart.Scripting
{
	public class RemoteShearingMinigameScript : RemoteScript
	{
		public override void Start(System.Action OnFinish)
		{
			Debug.Log("Shearing game!");
			ThirdWorldManager.Instance.UsedAction();
			Fader.FadeToBlack(0,2,"Shear the Sheep!!","Don't touch any space twice!",switchLevel);
			base.targetController.scripts.Clear();
			OnFinish();
		}

		public void switchLevel(){
			Application.LoadLevel("Shearing Minigame");
		}
	}
}
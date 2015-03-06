using UnityEngine;
using System.Collections;

namespace WorldsApart.Scripting
{
	public class RemoteMilkingMinigameScript : RemoteScript
	{
		public override void Start(System.Action OnFinish)
		{
			Debug.Log("Milk game!");
			ThirdWorldManager.Instance.UsedAction();
			Application.LoadLevel("Milking Minigame");

			base.targetController.scripts.Clear();
			OnFinish();
		}
	}
}
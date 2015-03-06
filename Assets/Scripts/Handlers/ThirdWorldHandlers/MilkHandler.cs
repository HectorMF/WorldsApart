using UnityEngine;
using System.Collections;


namespace WorldsApart.Handlers
{
	public class MilkHandler : Handler
	{
		public override void innerDelegate()
		{
			Debug.Log("Milk game!");
			ThirdWorldManager.Instance.UsedAction();
			Application.LoadLevel("Milking Minigame");
		}
	}
}
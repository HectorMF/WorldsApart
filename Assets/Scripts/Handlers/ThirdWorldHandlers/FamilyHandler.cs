using UnityEngine;
using System.Collections;

namespace WorldsApart.Handlers
{
	public class FamilyHandler : Handler 
	{
		public override void invoke()
		{
			Debug.Log("water family");
			if (ThirdWorldManager.Instance.CurrentWater >= 8 && ThirdWorldManager.Instance.TryAction())
			{
				ThirdWorldManager.Instance.IncrementMood();
				ThirdWorldManager.Instance.DecrementWater(8);
			}
			ThirdWorldManager.Instance.Report();
		}
	}
}

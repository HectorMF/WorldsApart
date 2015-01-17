using UnityEngine;
using System.Collections;

namespace WorldsApart.Handlers
{
	public class ChickenHandler : Handler
	{
		public override void invoke()
		{
			Debug.Log("water chicken");
			if (ThirdWorldManager.Instance.CurrentWater >= 3 && ThirdWorldManager.Instance.TryAction())
			{
				ThirdWorldManager.Instance.IncrementFood(2);
				ThirdWorldManager.Instance.DecrementWater(3);
			}
			ThirdWorldManager.Instance.Report();
		}
	}
}

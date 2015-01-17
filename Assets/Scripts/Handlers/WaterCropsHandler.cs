using UnityEngine;
using System.Collections;

namespace WorldsApart.Handlers
{
	public class WaterCropsHandler : Handler 
	{
		public override void invoke()
		{
			Debug.Log("water crops");
			if (ThirdWorldManager.Instance.CurrentWater > 0 && ThirdWorldManager.Instance.TryAction())
			{
				ThirdWorldManager.Instance.IncrementFood(2);
				ThirdWorldManager.Instance.DecrementWater(2);
			}
			ThirdWorldManager.Instance.Report();

		}
	}
}

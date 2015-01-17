using UnityEngine;
using System.Collections;

namespace WorldsApart.Handlers
{
	public class CropsHandler : Handler 
	{
        public override void innerDelegate()
		{
			Debug.Log("water crops");
			if (ThirdWorldManager.Instance.CurrentWater >= 10 && ThirdWorldManager.Instance.TryAction())
			{
				ThirdWorldManager.Instance.IncrementFood(8);
				ThirdWorldManager.Instance.DecrementWater(10);
			}
			ThirdWorldManager.Instance.Report();
		}
	}
}

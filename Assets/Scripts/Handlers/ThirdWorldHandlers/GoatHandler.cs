using UnityEngine;
using System.Collections;

namespace WorldsApart.Handlers
{
	public class GoatHandler : Handler 
	{
        public override void innerDelegate()
		{
			Debug.Log("water goat");
			if (ThirdWorldManager.Instance.CurrentWater >= 4 && ThirdWorldManager.Instance.TryAction())
			{
				ThirdWorldManager.Instance.IncrementFood(3);
				ThirdWorldManager.Instance.DecrementWater(4);
			}
			ThirdWorldManager.Instance.Report();
		}
	}
}

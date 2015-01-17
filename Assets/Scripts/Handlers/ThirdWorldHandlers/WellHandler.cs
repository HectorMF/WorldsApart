using UnityEngine;
using System.Collections;

namespace WorldsApart.Handlers
{
	public class WellHandler : Handler
	{
        public override void innerDelegate()
		{
			Debug.Log("get water from well");
			if (ThirdWorldManager.Instance.AvailableWater > 0 && ThirdWorldManager.Instance.TryAction())
			{
				ThirdWorldManager.Instance.GetWater();
			}
			ThirdWorldManager.Instance.Report();
		}
	}
}

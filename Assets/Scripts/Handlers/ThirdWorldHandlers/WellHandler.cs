using UnityEngine;
using System.Collections;

namespace WorldsApart.Handlers
{
	public class WellHandler : Handler
	{
        public override void innerDelegate()
		{
			if (ThirdWorldManager.Instance.AvailableWater > 0 && ThirdWorldManager.Instance.Actions > 0)
			{
				ThirdWorldManager.Instance.GetWater();
				gameObject.GetComponent<PumpAnimator>().Pump();
			}
			ThirdWorldManager.Instance.Report();
		}
	}
}

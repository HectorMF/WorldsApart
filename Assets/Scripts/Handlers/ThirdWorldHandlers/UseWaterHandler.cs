using UnityEngine;
using System.Collections;

namespace WorldsApart.Handlers
{
	public class UseWaterHandler : Handler 
	{
		public override void innerDelegate()
		{
			if (ThirdWorldManager.Instance.AnyWater && ThirdWorldManager.Instance.TryAction())
			{
				gameObject.GetComponent<Thirst>().Drink();
			}
			ThirdWorldManager.Instance.Report();
		}
	}
}

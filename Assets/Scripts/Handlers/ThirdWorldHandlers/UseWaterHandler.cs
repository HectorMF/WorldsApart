using UnityEngine;
using System.Collections;
using WorldsApart.Interfaces;

namespace WorldsApart.Handlers
{
	public class UseWaterHandler : Handler 
	{
		public override void invoke()
		{
			if (ThirdWorldManager.Instance.AnyWater && ThirdWorldManager.Instance.TryAction())
			{
				gameObject.GetComponent<Thirst>().Drink();
			}
			ThirdWorldManager.Instance.Report();
		}
	}
}

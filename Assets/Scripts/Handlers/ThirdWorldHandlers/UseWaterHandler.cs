using UnityEngine;
using System.Collections;

namespace WorldsApart.Handlers
{
	public class UseWaterHandler : Handler 
	{
		public override void innerDelegate()
		{
			if (CanAndShouldWater())
			{
				gameObject.GetComponent<Thirst>().Drink();
			}
			ThirdWorldManager.Instance.Report();
		}

		bool CanAndShouldWater()
		{
			return ThirdWorldManager.Instance.AnyWater 
					&& gameObject.GetComponent<Thirst>().AmountDrank < gameObject.GetComponent<Thirst>().TotalRequiredWater
					&& ThirdWorldManager.Instance.TryAction();
		}
	}
}

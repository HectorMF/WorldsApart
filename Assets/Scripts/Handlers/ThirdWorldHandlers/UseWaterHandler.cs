using UnityEngine;
using System.Collections;

namespace WorldsApart.Handlers
{
	public class UseWaterHandler : Handler 
	{
		public GameObject drinker;

		public override void innerDelegate()
		{
			if (CanAndShouldWater())
			{
				drinker.GetComponent<Thirst>().Drink();
			}
			ThirdWorldManager.Instance.Report();
		}

		bool CanAndShouldWater()
		{
			return ThirdWorldManager.Instance.AnyWater 
				&& drinker.GetComponent<Thirst> ().AmountDrank < drinker.GetComponent<Thirst> ().TotalRequiredWater;
//					&& ThirdWorldManager.Instance.Actions > 0;
		}
	}
}

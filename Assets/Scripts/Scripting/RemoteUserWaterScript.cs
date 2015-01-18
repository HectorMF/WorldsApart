using UnityEngine;
using System.Collections;
using System;

namespace WorldsApart.Scripting
{
	public class RemoteUseWaterScript : RemoteScript
	{
        public GameObject drinker;

		public override void Start(Action OnFinish)
		{
			if (CanAndShouldWater())
			{
				drinker.GetComponent<Thirst>().Drink();
			}
			ThirdWorldManager.Instance.Report();

            OnFinish();
		}

		bool CanAndShouldWater()
		{
			return ThirdWorldManager.Instance.AnyWater 
					&& drinker.GetComponent<Thirst>().AmountDrank < drinker.GetComponent<Thirst>().TotalRequiredWater
					&& ThirdWorldManager.Instance.TryAction();
		}
	}
}

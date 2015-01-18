using UnityEngine;
using System.Collections;

namespace WorldsApart.Handlers
{
	public class SideQuestHandler : Handler
	{
		public override void innerDelegate()
		{
			switch (ThirdWorldManager.Instance.CurrentWeather)
			{
			case ThirdWorldManager.Weather.Nice:
				gameObject.transform.Find("MiniGame").gameObject.SetActive(true);
				break;
			case ThirdWorldManager.Weather.Rainy:
				// family time
				ThirdWorldManager.Instance.IncrementMood();
				break;
			}
		}
	}
}
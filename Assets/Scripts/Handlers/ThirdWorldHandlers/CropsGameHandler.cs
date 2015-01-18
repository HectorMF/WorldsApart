using UnityEngine;
using System.Collections;

namespace WorldsApart.Handlers
{
	public class CropsGameHander : Handler 
	{
		public override void innerDelegate()
		{
			gameObject.GetComponent<CropsGameController>().StartGame();
		}
	}
}

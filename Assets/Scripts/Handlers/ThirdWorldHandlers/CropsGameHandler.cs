using UnityEngine;
using System.Collections;
using WorldsApart.Games.CropsMinigame;

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

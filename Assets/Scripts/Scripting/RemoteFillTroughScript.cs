using UnityEngine;
using System.Collections;
using System;

namespace WorldsApart.Scripting
{
	public class RemoteFillTroughScript : RemoteScript 
	{
		public GameObject selfGameObject;
		TroughManager tm;

		public override void Start(Action OnFinish)
		{
			Debug.Log("water the trough");
			tm = selfGameObject.GetComponent<TroughManager>();

			if (CanAndShouldWaterTrough())
			{
				tm.AddWaterToTrough();
			}
			ThirdWorldManager.Instance.Report();

			OnFinish();
		}
		
		bool CanAndShouldWaterTrough()
		{
			return ThirdWorldManager.Instance.AnyWater && (ThirdWorldManager.Instance.Actions > 0) && tm.NeedsWater();
		}
	}
}
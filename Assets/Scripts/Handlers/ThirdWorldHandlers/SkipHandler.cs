using UnityEngine;
using System.Collections;


namespace WorldsApart.Handlers
{
	public class SkipHandler : Handler
	{
        public override void innerDelegate()
		{
			Debug.Log("Do nothing");
			ThirdWorldManager.Instance.TryAction();
			ThirdWorldManager.Instance.Report();
		}
	}
}
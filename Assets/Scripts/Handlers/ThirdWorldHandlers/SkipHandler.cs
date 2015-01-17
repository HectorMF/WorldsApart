using UnityEngine;
using System.Collections;


namespace WorldsApart.Handlers
{
	public class SkipHandler : Handler
	{
		public override void invoke()
		{
			ThirdWorldManager.Instance.TryAction();
		}
	}
}
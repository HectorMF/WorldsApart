using UnityEngine;
using System.Collections;


namespace WorldsApart.Handlers
{
	public class SkipHandler : Handler
	{
        public override void innerDelegate()
		{
			ThirdWorldManager.Instance.TryAction();
		}
	}
}
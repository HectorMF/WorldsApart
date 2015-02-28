using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using WorldsApart.Utility;

namespace WorldsApart.Scripting
{
    public class WellScript : RemoteScript
    {
        public GameObject Well;

        public override void Start(Action OnFinish)
        {
            if (CanGetWater ()) 
			{
				ThirdWorldManager.Instance.GetWater ();
				Well.GetComponent<PumpAnimator> ().Pump ();
				ThirdWorldManager.Instance.UsedAction ();
			}
            ThirdWorldManager.Instance.Report();

            new Wait(OnFinish, 2.5f);
        }

		static bool CanGetWater ()
		{
			return ThirdWorldManager.Instance.AvailableWater > 0 && ThirdWorldManager.Instance.Actions > 0;
		}
    }
}

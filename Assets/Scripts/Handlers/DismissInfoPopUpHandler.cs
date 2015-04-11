using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WorldsApart.Handlers
{
	public class DismissInfoPopUpHandler : Handler {

		public GameObject ActionIndicator;
		public override void innerDelegate()
		{
			gameObject.transform.parent.gameObject.SetActive(false);
			Thirst thirst = gameObject.GetComponentInParent<Thirst> ();
			if (thirst != null && thirst.NeedsWater ()) {
				ActionIndicator.SetActive(true);
			}
		}
	}
}



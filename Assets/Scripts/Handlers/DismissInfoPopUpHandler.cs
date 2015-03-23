using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WorldsApart.Handlers
{
	public class DismissInfoPopUpHandler : Handler {

		public override void innerDelegate()
		{
			gameObject.transform.parent.gameObject.SetActive(false);
		}
	}
}



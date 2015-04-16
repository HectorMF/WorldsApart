using UnityEngine;
using System.Collections;
using WorldsApart.Clickables;
using WorldsApart.Scripting;

namespace WorldsApart.Handlers
{
	public class UseFoodHandler : Handler 
	{
		public GameObject eaterPopUp;
		
		public override void innerDelegate()
		{
			if (CanEat())
			{
				RemoteScriptHandler rsh = (RemoteScriptHandler)eaterPopUp.GetComponent<Clickable>().Handlers[0];
				RemoteOpenPopUpScript ropus = (RemoteOpenPopUpScript)rsh.scripts[0];
				ThirdWorldManager.Instance.DecrementFood(ropus.RequiredFood);
			}
			ThirdWorldManager.Instance.Report();
		}
		
		bool CanEat()
		{
			return ThirdWorldManager.Instance.CurrentFood > 0;
		}
	}
}

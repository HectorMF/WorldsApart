using UnityEngine;
using System.Collections;
using System;

namespace WorldsApart.Scripting
{
	public class RemoteOpenPopUpScript : RemoteScript 
	{
		public GameObject PopUp;

		public override void Start(Action OnFinish)
		{
			PopUp.SetActive(true);
		}
	}
}

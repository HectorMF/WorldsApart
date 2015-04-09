using UnityEngine;
using System.Collections;
using System;

namespace WorldsApart.Scripting
{
	public class RemoteOpenPopUpScript : RemoteScript 
	{
		public GameObject InfoPopUp, WaterIndicator;

		public override void Start(Action OnFinish)
		{
			InfoPopUp.SetActive(true);
			WaterIndicator.SetActive(false);
		}
	}
}

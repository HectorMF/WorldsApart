using UnityEngine;
using System.Collections;
using System;

namespace WorldsApart.Scripting
{
	public class LoadGameScript : RemoteScript
	{
		public GameObject gameObject;

		public override void Start(Action OnFinish)
		{
			gameObject.GetComponent<MiniGameLoader> ().Load ();
		}
	}
}
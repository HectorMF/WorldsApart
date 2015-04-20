using UnityEngine;
using System.Collections;
using System;

namespace WorldsApart.Scripting
{
	public class LoadGameScript : RemoteScript
	{
		public MiniGameLoader loader;
		public override void Start(Action OnFinish)
		{
			loader.Load();
		}
	}
}
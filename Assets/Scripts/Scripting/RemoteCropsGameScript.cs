using UnityEngine;
using System.Collections;

namespace WorldsApart.Scripting
{
    public class RemoteCropsGameScript : RemoteScript
	{
        public CropsGameController controller;

		public override void Start(System.Action OnFinish)
		{
			controller.StartGame();
            base.targetController.scripts.Clear();
		}
	}
}

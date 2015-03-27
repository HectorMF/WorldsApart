using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldsApart.Scripting;
using UnityEngine;
using WorldsApart.Scripting;

namespace WorldsApart.Handlers
{
    public class RemoteScriptHandler : Handler
    {
        public List<RemoteScript> scripts;
		private ScriptController mainChar;

        public override void innerDelegate()
        {
			mainChar = GameObject.Find("MainChar").GetComponent<ScriptController>();
            if (scripts == null) return;
            foreach (var script in scripts)
            {
				script.targetController = mainChar;
                if(script.targetController != null && script.targetController.scripts != null)
                    script.targetController.scripts.Add(script);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldsApart.Scripting;

namespace WorldsApart.Handlers
{
    public class RemoteScriptHandler : Handler
    {
        public List<RemoteScript> scripts;

        public override void innerDelegate()
        {
            if (scripts == null) return;
            foreach (var script in scripts)
            {
                if(script.targetController != null && script.targetController.scripts != null)
                    script.targetController.scripts.Add(script);
            }
        }
    }
}

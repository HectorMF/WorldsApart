using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldsApart.Scripting
{
    [Serializable]
    public abstract class RemoteScript : Script
    {
        public ScriptController targetController;

        public void QueueScript()
        {
            if (targetController != null && targetController.scripts != null) targetController.scripts.Add(this);
        }
    }
}

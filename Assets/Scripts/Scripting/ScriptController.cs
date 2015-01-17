using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vexe.Runtime.Types;

namespace WorldsApart.Scripting
{
    public class ScriptController : BetterBehaviour
    {
        public List<Script> scripts;
        public bool started;

        private int index = 0;

        void Update()
        {
            if (!started) NextScript();
            started = true;
        }

        public virtual void NextScript()
        {
            if (index < scripts.Count)
            {
                scripts[index].Start(() => { index++; NextScript(); });
            }
        }
    }
}

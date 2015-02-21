using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vexe.Runtime.Types;

namespace WorldsApart.Scripting
{
    public class ScriptController : BetterBehaviour
    {
        public List<Script> scripts = new List<Script>();
        public bool started;

        private int index = 0;

        void Update()
        {
            if (scripts != null && scripts.Count > 0)
            {
                if (!started) NextScript();
                started = true;
            }
            else
                started = false;
        }

        public virtual void NextScript()
        {
            if (index < scripts.Count)
            {
                if (scripts[index] != null)
                    scripts[index].Start(() => { index++; NextScript(); });
                else
                    UnityEngine.Debug.LogWarning("A script in the Script Controller was empty");
            }
            else
            {
                index = 0;
                started = false;
                scripts.Clear();
            }
        }

        internal void ResetQueue()
        {
            index = 0;
            started = false;
            scripts.Clear();
        }
    }
}

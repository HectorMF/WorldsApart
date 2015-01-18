using Assets.Scripts.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WorldsApart.Utility
{
    public class Wait
    {
        private float time;
        private float passed;
        private Action _action;

        public Wait(Action action, float time)
        {
            _action = action;
            this.time = time;
            passed = 0;
            UpdateHelper.OnUpdate += Update;
        }

        public void Update()
        {
            passed += UnityEngine.Time.deltaTime;
            if (passed >= time)
            {
                UpdateHelper.OnUpdate -= Update;
                _action();
            }
        }
    }
}

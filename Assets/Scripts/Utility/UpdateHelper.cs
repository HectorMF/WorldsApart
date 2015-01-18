using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Utility
{
    public class UpdateHelper : MonoBehaviour
    {
        public delegate void UpdateCall();
        public static event UpdateCall OnUpdate;

        void Update()
        {
            if (OnUpdate != null) OnUpdate();
        }
    }
}

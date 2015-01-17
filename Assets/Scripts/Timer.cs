using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using WorldsApart.Handlers;

namespace WorldsApart
{
    public class Timer : MonoBehaviour
    {
        public int actions = 1;
        public bool looping;

        public List<Handler> handlers;

        private int stepsLeft;

        void start()
        {
            stepsLeft = actions;
        }

        public void step()
        {
            stepsLeft--;
            if (stepsLeft == 0)
            {
                foreach (var handler in handlers)
                    handler.invoke();
                if (looping)
                    stepsLeft = actions;
            }
        }

        public void reset()
        {
            stepsLeft = actions;
        }
    }
}

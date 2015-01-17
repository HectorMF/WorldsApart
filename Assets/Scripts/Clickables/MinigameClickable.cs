using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldsApart.Handlers;

namespace WorldsApart.Clickables
{
    public class MinigameClickable : Clickable
    {
        public Counter counter;

        void Start()
        {
            foreach (Handler handler in Handlers)
            {
                handler.gameObject = gameObject;
                var cHandler = handler as IncrementCounterHandler;
                if (cHandler != null)
                    cHandler.counter = counter;
            }
        }
    }
}

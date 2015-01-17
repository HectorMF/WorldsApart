using UnityEngine;
using System.Collections.Generic;
using Vexe.Runtime.Types;

namespace WorldsApart
{

    public class Clickable : BetterBehaviour
    {
        public List<Handler> Handlers;

        void Start()
        {
            foreach (Handler handler in Handlers)
                handler.gameObject = gameObject;
        }

        void OnMouseUpAsButton()
        {
            foreach(Handler handler in Handlers)
                handler.invoke();
        }
    }

}

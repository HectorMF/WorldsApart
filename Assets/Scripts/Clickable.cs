using UnityEngine;
using System.Collections.Generic;

namespace WorldsApart
{

    public class Clickable : MonoBehaviour
    {
        public List<Handler> Handlers;

        void OnMouseUpAsButton()
        {
            foreach(Handler handler in Handlers)
                handler.invoke();
        }
    }

}

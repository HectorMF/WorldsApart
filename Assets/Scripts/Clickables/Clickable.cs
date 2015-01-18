using UnityEngine;
using System.Collections.Generic;
using Vexe.Runtime.Types;
using WorldsApart.Handlers;
using WorldsApart.Utility;

namespace WorldsApart.Clickables
{
    public class Clickable : BetterBehaviour
    {
        public List<Handler> Handlers;
        public bool active = true;

        void Start()
        {
            if (Handlers != null && Handlers.Count > 0)
            {
                foreach (Handler handler in Handlers)
                    handler.gameObject = gameObject;
            }
        }

        void OnMouseUpAsButton()
        {
            if (active)
            {
                foreach (Handler handler in Handlers)
                    handler.Invoke();
            }
            var moveHandler = new MoveHandler();
            var player = GameObject.Find("MainChar");

            var wanderComponent = player.GetComponent<Wander>();
            if (wanderComponent != null)
            {
                wanderComponent.enabled = false;
                moveHandler.onFinished = () => wanderComponent.enabled = true;
            }

            moveHandler.targetObject = player;
            moveHandler.target = transform.localPosition;
            
            moveHandler.Invoke();
        }

        public void AddHandler(Handler handler)
        {
            Handlers.Add(handler);
            handler.gameObject = gameObject;
        }
    }
}
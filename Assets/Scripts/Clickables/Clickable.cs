using UnityEngine;
using System.Collections.Generic;
using Vexe.Runtime.Types;
using WorldsApart.Handlers;
using WorldsApart.Utility;
using WorldsApart.Scripting;

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
            var player = GameObject.Find("MainChar");

            if (player != null)
            {
                var moveScript = new MoveToTransformScript();
                var wanderOffScript = new WanderScript();

                wanderOffScript.gameObject = player;
                wanderOffScript.value = false;

                var wanderScript = new WanderScript();
                wanderScript.gameObject = player;
                wanderScript.value = true;

                moveScript.gameObject = player;
                moveScript.target = transform;

                var controller = player.GetComponent<ScriptController>();
                controller.ResetQueue();
                controller.scripts.Add(wanderOffScript);
                controller.scripts.Add(moveScript);
                controller.scripts.Add(wanderScript);
            }

            if (active)
            {
                foreach (Handler handler in Handlers)
                    handler.Invoke();
            }
        }

        public void AddHandler(Handler handler)
        {
            Handlers.Add(handler);
            handler.gameObject = gameObject;
        }
    }
}
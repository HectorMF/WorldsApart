using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using WorldsApart.Scripting;
using WorldsApart.Utility;

namespace WorldsApart.Handlers
{
    public class MoveHandler : Handler
	{
		public Vector3 target;
        public GameObject targetObject;
        [HideInInspector]
        public Action onFinished;

        private Movement movement;
        private Wander wander;

        public override void innerDelegate()
        {
            if (wander == null && gameObject != null) wander = gameObject.GetComponent<Wander>();

            if(movement == null && gameObject != null) movement = gameObject.GetComponent<Movement>();

            Action onFinishWrapper;

            if (wander != null)
            {
                var wanderOffScript = new WanderScript();
                wanderOffScript.gameObject = gameObject;
                wanderOffScript.value = false;
                wanderOffScript.Start(null);

                    onFinishWrapper = () =>
                        {
                            var wanderOnScript = new WanderScript();
                            wanderOnScript.gameObject = gameObject;
                            wanderOnScript.value = true;
                            wanderOnScript.Start(onFinished);
                        };
            }
            else
                onFinishWrapper = onFinished;

            if (movement != null)
            {
                if (targetObject != null) movement.Move(targetObject.transform, onFinishWrapper);
                else movement.Move(target, onFinishWrapper);
            }
            else onFinished();
        }
    }
}

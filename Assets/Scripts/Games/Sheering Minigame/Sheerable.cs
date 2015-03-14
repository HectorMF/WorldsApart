using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WorldsApart.Games.SheeringMinigame
{
    [RequireComponent (typeof (SpriteRenderer))]
    public class Sheerable : MonoBehaviour
    {
        private SheeringController controller;

        public int index;
        public bool upperLeft;
        public bool upperRight;
        public bool lowerLeft;
        public bool lowerRight;

        public void setController(SheeringController controller)
        {
            this.controller = controller;
        }

        void OnMouseDown()
        {
            if (controller.ValidIndex(index))
            {
                Sheer();
                controller.setMouseActive(true);
            }
        }

        private void Sheer()
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;

            controller.registerSelected(index);
        }

        void OnMouseOver()
        {
            if (controller.getMouseActive() && controller.ValidIndex(index))
            {
                Sheer();
            }
        }

        void OnMouseUp()
        {
            controller.setMouseActive(false);
        }
    }
}

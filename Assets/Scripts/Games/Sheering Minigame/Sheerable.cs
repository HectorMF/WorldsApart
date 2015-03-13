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
            var sprite = gameObject.GetComponent<SpriteRenderer>();
            sprite.sprite = controller.getSheeredSprite();
            sprite.transform.Translate(new Vector3(0f, 0f, .1f));

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WorldsApart.Games.SheeringMinigame
{
    public class Sheerable : MonoBehaviour
    {
        private SheeringController controller;

        bool sheered;
        public int index;

        //public void AssignAdjacent(Direction dir, Sheerable sheerable)
        //{
        //    switch(dir)
        //    {
        //        case Direction.Down:
        //            down = sheerable;
        //            break;
        //        case Direction.Left:
        //            left = sheerable;
        //            break;
        //        case Direction.Right:
        //            right = sheerable;
        //            break;
        //        case Direction.Up:
        //            up = sheerable;
        //            break;
        //    }
        //}

        public void setController(SheeringController controller)
        {
            this.controller = controller;
        }

        //public Sheerable GetAdjacent(Direction dir)
        //{
        //    switch (dir)
        //    {
        //        case Direction.Down:
        //            return down;
        //        case Direction.Left:
        //            return left;
        //        case Direction.Right:
        //            return right;
        //        case Direction.Up:
        //            return up;
        //    }

        //    throw new Exception("Invalid Direction");
        //}

        void OnMouseUpAsButton()
        {
            if (controller.ValidIndex(index))
            {
                var renderer = gameObject.GetComponent<MeshRenderer>();
                if (renderer != null) renderer.material.color = Color.red;
                controller.registerSelected(index);
            }
        }
    }

    //public enum Direction
    //{
    //    Left,
    //    Right,
    //    Up,
    //    Down
    //}
}

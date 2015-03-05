using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WorldsApart.Games.SheeringMinigame
{
    public class SheeringController : MonoBehaviour
    {
        public int rows;
        public int columns;
        public float spriteHeight;
        public float spriteWidth;

        public Sheerable sheerablePrefab;
        public GameObject notSheerablePrefab;

        //private Sheerable[] contents;
        private int prevIndex;
        private List<int> path;
        private bool mouseIsActive;

        void Start()
        {
            prevIndex = -1;
            path = new List<int>();
            //contents = new Sheerable[columns * rows];

            for (int x = 0; x < columns; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    var block = Instantiate(sheerablePrefab, new Vector3(((float)x + .5f - ((float)columns / 2f)) * (.05f + spriteWidth) + transform.position.x, ((float)y + .5f - ((float)rows / 2f)) * (spriteHeight + .05f) + transform.position.y, transform.position.z), Quaternion.identity) as Sheerable;
                    block.transform.parent = transform;
                    block.setController(this);
                    block.index = y * columns + x;
                    
                    //if (contents[x * columns + y] != null)
                    //{
                    //    block.AssignAdjacent(Direction.Left, contents[(x-1) * columns + y]);
                    //    contents[(x - 1) * columns + y].AssignAdjacent(Direction.Right, block);
                    //}

                    //if (contents[x * columns + y - 1] != null)
                    //{
                    //    block.AssignAdjacent(Direction.Down, contents[x * columns + y - 1]);
                    //    contents[x * columns + y - 1].AssignAdjacent(Direction.Up, block);
                    //}

                    //contents[x * columns + y] = block;
                }
            }
        }

        public void setMouseActive(bool value)
        {
            mouseIsActive = value;
        }

        public bool getMouseActive()
        {
            return mouseIsActive;
        }

        public void registerSelected(int index)
        {
            path.Add(index);
            prevIndex = index;
            if (path.Count == rows * columns)
                Debug.Log("Winner winner chicken dinner");
            else if (!NodesAvailable(index))
                Debug.Log("You lost");
        }

        internal bool ValidIndex(int index)
        {
            if (prevIndex == -1) return true;
            //Look left
            if (prevIndex % columns != columns - 1 && prevIndex == index - 1) return true;
            //Look right
            if (prevIndex % columns != 0 && prevIndex == index + 1) return true;
            //Look down
            if (prevIndex < columns * (rows - 1) && prevIndex == index - columns) return true;
            //Look up
            if (prevIndex > columns && prevIndex == index + columns) return true;
            return false;
        }

        private bool NodesAvailable(int index)
        {
            if (index % columns != columns - 1 && !path.Contains(index + 1)) return true;
            if (index % columns != 0 && !path.Contains(index - 1)) return true;
            if (index < columns * (rows - 1) && !path.Contains(index + columns)) return true;
            if (index > columns && !path.Contains(index - columns)) return true;
            return false;
        }
    }
}

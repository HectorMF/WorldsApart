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

        void Start()
        {
            prevIndex = -1;
            //contents = new Sheerable[columns * rows];

            for (int x = 0; x < columns; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    var block = Instantiate(sheerablePrefab, new Vector3(((float)x + .5f - ((float)columns / 2f)) * (.05f + spriteWidth) + transform.position.x, ((float)y + .5f - ((float)rows / 2f)) * (spriteHeight + .05f) + transform.position.y, transform.position.z), Quaternion.identity) as Sheerable;
                    block.transform.parent = transform;
                    block.setController(this);
                    block.index = x * columns + y;
                    
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

        public void registerSelected(int index)
        {
            prevIndex = index;
        }

        internal bool ValidIndex(int index)
        {
            return prevIndex == index - 1 || prevIndex == index + 1 || prevIndex == index + columns || prevIndex == index - columns || prevIndex == -1;
        }
    }
}

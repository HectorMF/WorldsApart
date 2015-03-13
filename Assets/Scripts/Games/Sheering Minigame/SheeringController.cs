﻿using System;
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

        public List<Sheerable> sheerablePrefabs;
        public List<Sprite> notSheerablePrefabs;

        private int prevIndex;
        private List<int> path;
        private bool mouseIsActive;

        ScoreController scoreController;

        void Start()
        {
            GameObject scoreObject = GameObject.Find("ScoreController");
            if (scoreObject != null)
                scoreController = scoreObject.GetComponent<ScoreController>();

            prevIndex = -1;
            path = new List<int>();

            for (int x = 0; x < columns; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    var prefab = sheerablePrefabs[UnityEngine.Random.Range(0, sheerablePrefabs.Count)];
                    var block = Instantiate(prefab, new Vector3(((float)x + .5f - ((float)columns / 2f)) * spriteWidth + transform.position.x, ((float)y + .5f - ((float)rows / 2f)) * spriteHeight + transform.position.y, transform.position.z), Quaternion.identity) as Sheerable;
                    block.transform.parent = transform;
                    block.setController(this);
                    block.index = y * columns + x;
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
            {
                if (scoreController != null) scoreController.Mood = 2;
                EndGame("You Win!!!");
            }
            else if (!NodesAvailable(index))
            {
                if (scoreController != null) scoreController.Mood = 0;
                EndGame("You Lost.");
            }
        }

        internal bool ValidIndex(int index)
        {
            if (prevIndex == -1) return true;
            if(path.Contains(index)) return false;
            //Look left
            if (prevIndex % columns != columns - 1 && prevIndex == index - 1) return true;
            //Look right
            if (prevIndex % columns != 0 && prevIndex == index + 1) return true;
            //Look up
            if (prevIndex < columns * (rows - 1) && prevIndex == index - columns) return true;
            //Look down
            if (prevIndex > columns - 1 && prevIndex == index + columns) return true;
            return false;
        }

        private bool NodesAvailable(int index)
        {
            if (index % columns != columns - 1 && !path.Contains(index + 1)) return true;
            if (index % columns != 0 && !path.Contains(index - 1)) return true;
            if (index < columns * (rows - 1) && !path.Contains(index + columns)) return true;
            if (index > columns - 1 && !path.Contains(index - columns)) return true;
            return false;
        }

        internal Sprite getSheeredSprite()
        {
            return notSheerablePrefabs[UnityEngine.Random.Range(0, notSheerablePrefabs.Count)];
        }

        private void EndGame(string text)
        {
			Fader.FadeToBlack(0,2,text,"",switchLevel);
        }

        public void switchLevel()
        {
            Application.LoadLevel("WorldsApartAgain");
        }
    }
}

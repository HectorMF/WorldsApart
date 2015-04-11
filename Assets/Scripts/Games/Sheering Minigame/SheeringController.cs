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
        private static List<string> puzzles = new List<string> 
        {
            "000000000001111100001000001000000000010000010000001110011000000000",
            "000001000000111110001000001001100000010001010000111110011100000000",
            "000000000000001100000010000000100001110011000001100110000011001111",
            "0011000000000000000000000000100000000000011000000000000111000000000",
            "000000000001100000000000110000000000000001100000000011010000000000"
        };
        private string puzzle;

        public List<Sheerable> sheerablePrefabs;

        private int prevIndex;
        private List<int> path;
        private bool mouseIsActive;

        ScoreController scoreController;

        void Start()
        {
            Fader.FadeToClear(Fader.Gesture.Swipe, 2, 2, "Shear the Sheep", "Don't touch any tuft twice");
            puzzle = puzzles[UnityEngine.Random.Range(0, puzzles.Count)];

            GameObject scoreObject = GameObject.Find("ScoreController");
            if (scoreObject != null)
                scoreController = scoreObject.GetComponent<ScoreController>();

            prevIndex = -1;
            path = new List<int>();

            for (int x = 0; x < columns; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    if (puzzle[y * columns + x] == '1') continue;
                    var suitable = sheerablePrefabs;
                    if (x == 0 && y == 0) suitable = sheerablePrefabs.Where(p => p.lowerLeft).ToList();
                    if (x == 0 && y == rows - 1) suitable = sheerablePrefabs.Where(p => p.upperLeft).ToList();
                    if (x == columns - 1 && y == rows - 1) suitable = sheerablePrefabs.Where(p => p.upperRight).ToList();
                    if (x == columns - 1 && y == 0) suitable = sheerablePrefabs.Where(p => p.lowerRight).ToList();

                    var prefab = suitable[UnityEngine.Random.Range(0, suitable.Count)];

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
            if (path.Count == rows * columns - puzzle.Count(c => c=='1'))
            {
                if (scoreController != null) scoreController.Mood = 2;
                EndGame("You Win!!!", true);
            }
            else if (!NodesAvailable(index))
            {
                if (scoreController != null) scoreController.Mood = 0;
                EndGame("You Lost.", false);
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
            if (index % columns != columns - 1 && !path.Contains(index + 1) && puzzle[index+1] != '1') return true;
            if (index % columns != 0 && !path.Contains(index - 1) && puzzle[index - 1] != '1') return true;
            if (index < columns * (rows - 1) && !path.Contains(index + columns) && puzzle[index + columns] != '1') return true;
            if (index > columns - 1 && !path.Contains(index - columns) && puzzle[index - columns] != '1') return true;
            return false;
        }

        private void EndGame(string text, bool win)
        {
            if (win)
            {
                ThirdWorldManager.Instance.IncrementMood();
            }
            else
                ThirdWorldManager.Instance.DecrementMood();
			Fader.FadeToBlack(0,2,text,"",switchLevel);
        }

        public void switchLevel()
        {
            Application.LoadLevel("WorldsApart");
        }
    }
}

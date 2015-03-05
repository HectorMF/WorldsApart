using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldsApart.Games.SheeringMinigame
{
    public class SheepPuzzleGenerator
    {
        private int rows;
        private int columns;
        private int difficulty;
        private int difficultyModifer = 50;

        private List<bool[,]> puzzles;

        public SheepPuzzleGenerator(int rows, int columns, int difficulty, int difficultyMod)
        {
            this.rows = rows;
            this.columns = columns;
            this.difficulty = difficulty;
            difficultyModifer = difficultyMod;
        }

        public void GeneratePuzzles()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    for (int k = 0; k < rows; k++)
                    {
                        
                    }
                }
            }
        }
    }
}

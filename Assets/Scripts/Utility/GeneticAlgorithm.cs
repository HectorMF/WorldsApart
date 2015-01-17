using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldsApart.Puzzles;

namespace WorldsApart.Utility
{
    public static class GeneticAlgorithm
    {
        public static float getScore(GeneticPuzzle puzzle)
        {
            puzzle.setInitialState();
            float topScore = puzzle.getCurrentScore();
            float puzzleScore;

            puzzle.getBestNextState();
            puzzleScore = puzzle.getCurrentScore();

            while(topScore != puzzleScore)
            {
                topScore = puzzleScore;
                puzzle.getBestNextState();
                puzzleScore = puzzle.getCurrentScore();
                UnityEngine.Debug.Log(topScore);
            }

            UnityEngine.Debug.Log(topScore);
            return puzzle.getCurrentScore();
        }
    }
}

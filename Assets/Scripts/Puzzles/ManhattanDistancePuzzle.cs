using Assets.Scripts.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WorldsApart.Puzzles
{
    public abstract class ManhattanDistancePuzzle : GeneticPuzzle
    {
        public List<Tuple<Vector2, Vector2>> mappings;

        public override float getCurrentScore()
        {
            float sum = 0;

            foreach (var tuple in mappings)
            {
                sum += Math.Abs(tuple.item1.x - tuple.item2.x) + Math.Abs(tuple.item1.y - tuple.item2.y);
            }

            return sum;
        }

        private float getCurrentScore(List<Tuple<Vector2, Vector2>> map)
        {
            float sum = 0;

            foreach (var tuple in map)
            {
                sum += Math.Abs(tuple.item1.x - tuple.item2.x) + Math.Abs(tuple.item1.y - tuple.item2.y);
            }

            return sum;
        }

        public override void getBestNextState()
        {
            var nextMapping = mappings;

            for(int i = 0; i < mappings.Count; i++)
                for (int j = 0; j < mappings.Count; j++)
                {
                    if (j != i)
                    {
                        List<Tuple<Vector2, Vector2>> tempMap = new List<Tuple<Vector2,Vector2>>();
                        
                        foreach(var tuple in mappings)
                            tempMap.Add(new Tuple<Vector2,Vector2> (tuple.item1, tuple.item2));

                        var tempItem = tempMap[i].item2;
                        tempMap[i].item2 = tempMap[j].item2;
                        tempMap[j].item2 = tempItem;

                        var tempScore = getCurrentScore(tempMap);
                        var getBestNextState = getCurrentScore(nextMapping);

                        if (tempScore < getBestNextState)
                            nextMapping = tempMap;
                    }
                }

            mappings = nextMapping;
        }
    }
}

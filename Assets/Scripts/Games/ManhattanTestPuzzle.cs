using Assets.Scripts.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using WorldsApart.Utility;

namespace WorldsApart.Puzzles
{
    public class ManhattanTestPuzzle : ManhattanDistancePuzzle
    {
        void Start()
        {
            mappings = new List<Tuple<Vector2, Vector2>>();
        }

        public override void setInitialState()
        {
            mappings.Add(new Tuple<Vector2, Vector2> (new Vector2(1,0), new Vector2(0,2)));
            mappings.Add(new Tuple<Vector2, Vector2> (new Vector2(1,2), new Vector2(3,1)));
            mappings.Add(new Tuple<Vector2, Vector2> (new Vector2(4,2), new Vector2(6,0)));
            mappings.Add(new Tuple<Vector2, Vector2> (new Vector2(7,1), new Vector2(7,3)));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vexe.Runtime.Types;

namespace WorldsApart.Puzzles
{
    public abstract class GeneticPuzzle : BetterBehaviour
    {
        public abstract void setInitialState();

        public abstract float getCurrentScore();

        public abstract void getBestNextState();
    }

}